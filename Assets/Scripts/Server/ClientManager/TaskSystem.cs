using Unity.Netcode;
using UnityEngine;

public partial class ClientsManager
{
    public void InitTaskSystem()
    {
        NetMessageManager.Instance.RegisterOnReceiveMessageCallback(NetMessageType.C2S_GetTaskData, OnReceiveGetTaskDataMessage);
        NetMessageManager.Instance.RegisterOnReceiveMessageCallback(NetMessageType.C2S_CompeleteTask, OnReceiveCompeleteTaskMessage);
    }

    private void OnReceiveCompeleteTaskMessage(ulong clientId, INetworkSerializable serializable)
    {
        C2S_CompeleteTask message = (C2S_CompeleteTask)serializable;
        Client client = clientIdDic[clientId];
        PlayerData playerData = client.playerData;
        int index = message.index;
        CheckAndCompeleteTask(clientId, index);
    }

    public void CheckAndCompeleteTask(ulong clientId, int index)
    {
        Client client = clientIdDic[clientId];
        PlayerData playerData = client.playerData;
        if (index < 0 || index >= playerData.taskDatas.taskDataList.Count) return;

        TaskData taskData = playerData.taskDatas.taskDataList[index];
        TaskConfig taskConfig = ServerResSystem.GetTaskConfig(taskData.taskConfigId);
        TaskInfoBase taskInfo = taskConfig.taskInfo;
        if (taskInfo is DialogTaskInfo)
        {
            playerData.taskDatas.CompeleteTask(index);
            // 获取奖励 
            GiveReward(clientId, taskConfig);
            AddAndCheckNextTask(clientId, taskConfig);

            NetMessageManager.Instance.SendMessageToClient<S2C_GetTaskData>(clientId, NetMessageType.S2C_GetTaskData, new S2C_GetTaskData()
            {
                version = playerData.taskDatas.version,
                taskDatas = playerData.taskDatas
            });
        }
        else if (taskInfo is CollectItemTaskInfo collectTaskInfo)
        {
            // 对于收集任务，最终确认一次进度是否达标
            if (taskData.progress >= collectTaskInfo.count)
            {
                // TODO: 如果需求要求提交任务时扣除物品，需要在此处遍历并调用 playerData.bagData.RemoveItem 进行扣除
                // 如果只要求"拥有过"即可，则不扣除

                playerData.taskDatas.CompeleteTask(index);
                GiveReward(clientId, taskConfig);
                AddAndCheckNextTask(clientId, taskConfig);

                NetMessageManager.Instance.SendMessageToClient<S2C_GetTaskData>(clientId, NetMessageType.S2C_GetTaskData, new S2C_GetTaskData()
                {
                    version = playerData.taskDatas.version,
                    taskDatas = playerData.taskDatas
                    
                });
            }
        }
        else if (taskInfo is StruckDownTaskInfo)
        {
            // 击杀任务完成逻辑
        }
    }

    //================ 新增区域 =====================//

    /// <summary>
    /// 当玩家背包物品产生变动（如拾取、购买、丢弃使用）时，调用此方法来刷新收集任务进度
    /// </summary>
    public void UpdateCollectTaskProgress(ulong clientId)
    {
        if (!clientIdDic.TryGetValue(clientId, out Client client)) return;
        PlayerData playerData = client.playerData;

        // 遍历更新所有任务
        for (int i = playerData.taskDatas.taskDataList.Count - 1; i >= 0; i--)
        {
            TaskData taskData = playerData.taskDatas.taskDataList[i];
            TaskConfig taskConfig = ServerResSystem.GetTaskConfig(taskData.taskConfigId);

            // 筛选出收集任务
            if (taskConfig.taskInfo is CollectItemTaskInfo collectTaskInfo)
            {
                int currentItemCount = GetItemCountInBag(playerData.bagData, collectTaskInfo.itemId);

                // 将进度限制在最大需求值，超过最大值也只显示最大值
                int progress = Mathf.Min(currentItemCount, collectTaskInfo.count);
                taskData.progress = progress; // 先更新进度，再确认是否达标
                // 确认一次进度是否达标
                if (taskData.progress >= collectTaskInfo.count)
                {
                    CheckAndCompeleteTask(clientId, i);
                }
                // 如果未达标，更新进度
                else
                {
                    taskData.progress = progress;
                    playerData.taskDatas.AddDataVersion(); // 维护版本号

                    NetMessageManager.Instance.SendMessageToClient<S2C_UpdateTaskData>(clientId, NetMessageType.S2C_UpdateTaskData, new S2C_UpdateTaskData()
                    {
                        data = taskData,
                        version = playerData.taskDatas.version
                    });
                }
            }
        }
    }

    public void AddAndCheckNextTask(ulong clientId, TaskConfig taskConfig)
    {
        Client client = clientIdDic[clientId];
        PlayerData playerData = client.playerData;
        playerData.taskDatas.AddNextTask(taskConfig);
        UpdateCollectTaskProgress(clientId); // 新增任务后立即检查一次收集任务的进度，防止客户端背包已有足够物品但未更新任务进度的情况
    }

    /// <summary>
    /// 获取背包中某个ID物品的总数量
    /// </summary>
    private int GetItemCountInBag(BagData bagData, string itemId)
    {
        int count = 0;
        foreach (var item in bagData.itemDataList)
        {
            if (item != null && item.id == itemId)
            {
                if (item is StackableItemDataBase stackable)
                    count += stackable.count;
                else
                    count += 1;
            }
        }
        return count;
    }

    private void OnReceiveGetTaskDataMessage(ulong clientId, INetworkSerializable serializable)
    {
        C2S_GetTaskData message = (C2S_GetTaskData)serializable;
        Client client = clientIdDic[clientId];
        PlayerData playerData = client.playerData;
        if (message.version != playerData.taskDatas.version)
        {
            S2C_GetTaskData s2C_GetTaskData = new S2C_GetTaskData()
            {
                version = playerData.taskDatas.version
            };
            s2C_GetTaskData.taskDatas = playerData.taskDatas;
            s2C_GetTaskData.taskDatas.AddTask(new TaskData() { taskConfigId = "Task_1", progress = 0 });
            s2C_GetTaskData.taskDatas.AddTask(new TaskData() { taskConfigId = "Task_6", progress = 0 });
            NetMessageManager.Instance.SendMessageToClient<S2C_GetTaskData>(clientId, NetMessageType.S2C_GetTaskData, s2C_GetTaskData);
        }
    }

    // 给怪物死亡时调用的公共方法
    public void OnPlayerKillMonster(ulong killerClientId, string monsterConfigId)
    {
        if (!clientIdDic.TryGetValue(killerClientId, out Client client)) return;

        PlayerData playerData = client.playerData;

        // 遍历玩家当前身上的所有任务
        for (int i = playerData.taskDatas.taskDataList.Count - 1; i >= 0; i--)
        {
            TaskData taskData = playerData.taskDatas.taskDataList[i];
            TaskConfig taskConfig = ServerResSystem.GetTaskConfig(taskData.taskConfigId);

            // 筛选出击杀任务
            if (taskConfig.taskInfo is StruckDownTaskInfo killTaskInfo)
            {
                // 如果杀的怪物ID匹配且进度没有满
                if (killTaskInfo.monsterId == monsterConfigId && taskData.progress < killTaskInfo.count)
                {
                    taskData.progress++;
                    playerData.taskDatas.AddDataVersion(); // 维护版本号
                    if (taskData.progress == killTaskInfo.count) // 如果达到完成条件
                    {
                        playerData.taskDatas.CompeleteTask(i);
                        // 获取奖励 
                        GiveReward(killerClientId, taskConfig);
                        playerData.taskDatas.AddNextTask(taskConfig);

                        NetMessageManager.Instance.SendMessageToClient<S2C_GetTaskData>(killerClientId, NetMessageType.S2C_GetTaskData, new S2C_GetTaskData()
                        {
                            version = playerData.taskDatas.version,
                            taskDatas = playerData.taskDatas
                        });
                    }
                    else
                    {
                        // 进度有变化，同步给客户端
                        NetMessageManager.Instance.SendMessageToClient<S2C_UpdateTaskData>(killerClientId, NetMessageType.S2C_UpdateTaskData, new S2C_UpdateTaskData()
                        {
                            data = taskData,
                            version = playerData.taskDatas.version
                        });
                    }
                }
            }
        }
    }

    private void GiveReward(ulong clientId, TaskConfig taskConfig)
    {
        Client client = clientIdDic[clientId];
        PlayerData playerData = client.playerData;
        TaskRewardBase reward = taskConfig.taskReward;
        if (reward is CoinTaskReward)
        {
            playerData.bagData.money += ((CoinTaskReward)reward).coinCount;
            NetMessageManager.Instance.SendMessageToClient<S2C_BagUpdateMoney>(clientId, NetMessageType.S2C_BagUpdateMoney, new S2C_BagUpdateMoney
            {
                money = playerData.bagData.money
            });
            NetMessageManager.Instance.SendMessageToClient<S2C_GetMoneyReward>(clientId, NetMessageType.S2C_GetMoneyReward, new S2C_GetMoneyReward
            {
                count = ((CoinTaskReward)reward).coinCount
            });
        }
    }
}
