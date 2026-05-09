using Cinemachine;
using JKFrame;
using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;

public class PlayerManager : SingletonMono<PlayerManager>
{
    public static PlayerController playerController;
    public static PlayerClientController playerClientController;
    public static BagData bagData;
    public static TaskDatas taskDatas;
    public static string currentMerchantConfig;
    public static string currentCrafterConfig;
    [SerializeField] private CinemachineFreeLook cinemachineFreeLook;
    private bool requestBagWindow;
    private bool requestShopWindow;
    private bool requestCraftWindow;
    public CinemachineFreeLook FreeLook => cinemachineFreeLook;
    public bool RequestShopWindow => requestShopWindow;
    public bool RequestBagWindow => requestBagWindow;
    public bool RequestCraftWindow => requestCraftWindow;
    
    public void Init()
    {
        EventSystem.AddTypeEventListener<PlayerSpawnEvent>(OnPlayerSpawn);
        PlayerController.SetGetWeaponFunc(GetWeapon);
        NetMessageManager.Instance.RegisterOnReceiveMessageCallback(NetMessageType.S2C_GetBagData, OnReceiveBagData);
        NetMessageManager.Instance.RegisterOnReceiveMessageCallback(NetMessageType.S2C_BagUpdateItem, OnReceiveUpdateBagData);
        NetMessageManager.Instance.RegisterOnReceiveMessageCallback(NetMessageType.S2C_ChangeShortCutIndex, OnReceiveChangeShortCutIndex);
        NetMessageManager.Instance.RegisterOnReceiveMessageCallback(NetMessageType.S2C_BagExchangeItem, OnReceiveBagExchangeItem);
        NetMessageManager.Instance.RegisterOnReceiveMessageCallback(NetMessageType.S2C_BagUpdateMoney, OnReceiveBagUpdateMoney);
        NetMessageManager.Instance.RegisterOnReceiveMessageCallback(NetMessageType.S2C_GetTaskData, OnReceiveTaskData);
        NetMessageManager.Instance.RegisterOnReceiveMessageCallback(NetMessageType.S2C_UpdateTaskData, OnReceiveUpdateTaskData);
        NetMessageManager.Instance.RegisterOnReceiveMessageCallback(NetMessageType.S2C_GetMoneyReward, OnReceiveGetMoneyReward);
        ShowShortCutBat();
        RequestTaskDatas();
    }

    private void OnDestroy()
    { 
        EventSystem.RemoveTypeEventListener<PlayerSpawnEvent>(OnPlayerSpawn);
        NetMessageManager.Instance.UnRegisterOnReceiveMessageCallback(NetMessageType.S2C_GetBagData, OnReceiveBagData);
        NetMessageManager.Instance.UnRegisterOnReceiveMessageCallback(NetMessageType.S2C_BagUpdateItem, OnReceiveUpdateBagData);
        NetMessageManager.Instance.UnRegisterOnReceiveMessageCallback(NetMessageType.S2C_ChangeShortCutIndex, OnReceiveChangeShortCutIndex);
        NetMessageManager.Instance.UnRegisterOnReceiveMessageCallback(NetMessageType.S2C_BagExchangeItem, OnReceiveBagExchangeItem);
        NetMessageManager.Instance.UnRegisterOnReceiveMessageCallback(NetMessageType.S2C_BagUpdateMoney, OnReceiveBagUpdateMoney);
        NetMessageManager.Instance.UnRegisterOnReceiveMessageCallback(NetMessageType.S2C_GetTaskData, OnReceiveTaskData);
        NetMessageManager.Instance.UnRegisterOnReceiveMessageCallback(NetMessageType.S2C_UpdateTaskData, OnReceiveUpdateTaskData);
        NetMessageManager.Instance.UnRegisterOnReceiveMessageCallback(NetMessageType.S2C_GetMoneyReward, OnReceiveGetMoneyReward);
        CloseShortCutBar();
    }

    

    private void Update()
    {
        UpdateOpenBag();
        UpdateOpenShop();
        UpdateOpenCraft();
        UpdateHandleShortCutInput();
    }

    #region 任务
    private void OnReceiveTaskData(ulong clientId, INetworkSerializable serializable)
    {
        S2C_GetTaskData message = (S2C_GetTaskData)serializable;
        taskDatas = message.taskDatas;
        taskDatas.version = message.version;
        UI_TaskWindow window = UISystem.Show<UI_TaskWindow>();
        window.onTaskBeClickAction = FindTaskPath;
        window.onTaskEndAction = PathLineGuide.Instance.HidePath;
        window.BindAction();
    }

    private void FindTaskPath(TaskConfig config)
    {
        if (config == null || playerClientController == null || playerClientController.navMeshAgent == null)
        {
            PathLineGuide.Instance.HidePath();
            return;
        }

        NavMeshAgent agent = playerClientController.navMeshAgent;
        if (!agent.enabled)
        {
            PathLineGuide.Instance.HidePath();
            return;
        }

        Vector3 rawStart = playerClientController.transform.position;
        Vector3 rawTarget = config.targetPos;

        if (!NavMesh.SamplePosition(rawStart, out NavMeshHit startHit, 6f, NavMesh.AllAreas))
        {
            PathLineGuide.Instance.HidePath();
            return;
        }

        if (!NavMesh.SamplePosition(rawTarget, out NavMeshHit targetHit, 12f, NavMesh.AllAreas))
        {
            PathLineGuide.Instance.HidePath();
            return;
        }

        NavMeshPath path = new NavMeshPath();
        bool ok = NavMesh.CalculatePath(startHit.position, targetHit.position, NavMesh.AllAreas, path);
        if (!ok || path.status == NavMeshPathStatus.PathInvalid || path.corners == null || path.corners.Length < 2)
        {
            PathLineGuide.Instance.HidePath();
            return;
        }

        // PathComplete / PathPartial 都绘制，至少给玩家方向指引
        PathLineGuide.Instance.DrawPath(path.corners);
    }

    public void DialogTaskCompeleted(int index)
    {
        TaskData taskData = PlayerManager.taskDatas.taskDataList[index];
        TaskConfig taskConfig = ResSystem.LoadAsset<TaskConfig>(taskData.taskConfigId);
        TaskInfoBase taskInfo = taskConfig.taskInfo;
        if (taskInfo is DialogTaskInfo)
        {
             NetMessageManager.Instance.SendMessageToServer<C2S_CompeleteTask>(NetMessageType.C2S_CompeleteTask, new C2S_CompeleteTask
             {
                 index = index
             });
        }
    }
    #endregion

    #region 服务端交互

    private void OnReceiveGetMoneyReward(ulong clientId, INetworkSerializable serializable)
    {
        UISystem.Show<UI_GetRewardWindow>().Show(new RewardData
        {
            iconKey = "coin",
            count = ((S2C_GetMoneyReward)serializable).count
        });
    }

    private void OnReceiveUpdateTaskData(ulong clientId, INetworkSerializable serializable)
    {
        S2C_UpdateTaskData message = (S2C_UpdateTaskData)serializable;
        if (taskDatas.version == message.version) return;
        taskDatas.version = message.version;
        for(int i = 0;i < taskDatas.taskDataList.Count; ++i)
        {
            if (taskDatas.taskDataList[i].taskConfigId == message.data.taskConfigId)
            {
                taskDatas.taskDataList[i] = message.data;
                break;
            }
        }
        UISystem.Show<UI_TaskWindow>();
    }
    private void RequestTaskDatas()
    {
        NetMessageManager.Instance.SendMessageToServer<C2S_GetTaskData>(NetMessageType.C2S_GetTaskData, new C2S_GetTaskData
        {
            version = taskDatas == null ? -1 : taskDatas.version
        });
    }
    private void OnReceiveBagUpdateMoney(ulong clientId, INetworkSerializable serializable)
    {
        S2C_BagUpdateMoney message = (S2C_BagUpdateMoney)serializable;
        if (bagData != null)
        {
            bagData.money = message.money;
            if (UISystem.GetWindow<UI_BagWindow>() != null && UISystem.GetWindow<UI_BagWindow>().gameObject.activeInHierarchy)
            {
                UISystem.GetWindow<UI_BagWindow>().UpdateMoney(message.money);
            }
        }
    }

    private void OnReceiveBagExchangeItem(ulong clientId, INetworkSerializable serializable)
    {
        S2C_BagExchangeItem message = (S2C_BagExchangeItem)serializable;
        bagData.ExchangeItem(message.oldIndex, message.newIndex);
        UISystem.GetWindow<UI_BagWindow>().UpdataItem(message.oldIndex, bagData.itemDataList[message.oldIndex]);
        UISystem.GetWindow<UI_BagWindow>().UpdataItem(message.newIndex, bagData.itemDataList[message.newIndex]);
    }

    private void OnReceiveChangeShortCutIndex(ulong clientId, INetworkSerializable serializable)
    {
        S2C_ChangeShortCutIndex message = (S2C_ChangeShortCutIndex)serializable;
        bagData.SetShortCut(message.shortCutIndex, message.itemIndex);
        UISystem.GetWindow<UI_ShortCutBarWindow>().UpdateItem(message.shortCutIndex);
    }

    private void OnReceiveBagData(ulong clientId, INetworkSerializable serializable)
    {
        S2C_GetBagData s2C_GetBagData = (S2C_GetBagData)serializable;
        if (s2C_GetBagData.haveBag) bagData = s2C_GetBagData.bagData;
        if (requestBagWindow)
        {
            UISystem.Show<UI_BagWindow>().Show(bagData);
            requestBagWindow = false;
        }
        if (UISystem.GetWindow<UI_ShortCutBarWindow>() == null)
        {
            UISystem.Show<UI_ShortCutBarWindow>().Show(bagData);
        }
        else
        {
            UISystem.GetWindow<UI_ShortCutBarWindow>().Show(bagData);
        }
    }

    private void OnReceiveUpdateBagData(ulong clientId, INetworkSerializable serializable)
    {
        S2C_BagUpdateItem message = (S2C_BagUpdateItem)serializable;
        // 如果版本相同 不需要操作
        if (bagData == null || bagData.version == message.version) return;
        bagData.version = message.version;
        if (message.isUse && message.itemType == ItemType.Weapon) bagData.usedWeponIndex = message.index;
        bagData.itemDataList[message.index] = message.itemData;
        // 如果背包窗口存在 更新背包对应物体的表现
        if (UISystem.GetWindow<UI_BagWindow>() != null && UISystem.GetWindow<UI_BagWindow>().gameObject.activeInHierarchy)
        {
            UISystem.GetWindow<UI_BagWindow>().UpdataItem(message.index, message.itemData);
            if (message.isUse && message.itemType == ItemType.Weapon) // 如果是武器，切换UsedIcon 
            {
                UISystem.GetWindow<UI_BagWindow>().UpdateWeaponUsedIcon(message.oldIndex, message.index);
            }

        }
        if (UISystem.GetWindow<UI_ShortCutBarWindow>() != null && UISystem.GetWindow<UI_ShortCutBarWindow>().gameObject.activeInHierarchy)
        {
            UISystem.GetWindow<UI_ShortCutBarWindow>().Show(bagData);
        }
    }
    #endregion

    #region 锻造
    public void UpdateOpenCraft()
    {
        if (requestCraftWindow)
        {
            CrafterConfig config = ResSystem.LoadAsset<CrafterConfig>(currentCrafterConfig);
            if (!ClientUtility.UIWindowExist<UI_CraftWindow>())
            {
                UISystem.Show<UI_CraftWindow>().Show(config);
                if (!ClientUtility.UIWindowExist<UI_BagWindow>())
                {
                    requestBagWindow = true;
                    RequestBagData();
                } // 同时打开Craft和Bag
            }
            else
            {
                UISystem.Close<UI_CraftWindow>();
                UISystem.Close<UI_BagWindow>();
            }
            requestCraftWindow = false;
        }
    }

    public void RequestOpenCraft(string crafterConfig)
    {
        currentCrafterConfig = crafterConfig;
        requestCraftWindow = true;
    }

    public void CraftItem(ItemDataBase itemDataBase)
    {
        string itemId = itemDataBase.id;
        int count = itemDataBase is StackableItemDataBase stackableItemData ? stackableItemData.count : 1;
        ItemConfigBase itemConfig = ResSystem.LoadAsset<ItemConfigBase>(itemId);
        foreach (var item in itemConfig.craftItemDic)
        {
            if (!bagData.CheckHasItem(item.Key, item.Value, out int index))
            {
                UISystem.Show<UI_MessagePopUp>().ShowMessage(LocalizationKey.materialLack, Color.yellow);
                return;
            }
        }
        int bagIndex = -1;
        bagData.TryGetItemLayPos(itemId, out bagIndex);
        if (bagIndex == -1)
        {
            UISystem.Show<UI_MessagePopUp>().ShowMessage(LocalizationKey.bagSpaceLack, Color.yellow);
            return;
        }
        NetMessageManager.Instance.SendMessageToServer<C2S_CraftItem>(NetMessageType.C2S_CraftItem, new C2S_CraftItem
        {
            itemId = itemId,
            count = count,
            bagIndex = bagIndex
        });
    }
    #endregion

    #region 商店
    public void RequestOpenShop(string merchantConfig)
    {
        currentMerchantConfig = merchantConfig;
        requestShopWindow = true;
    }

    public void ShopBuyItem(string itemId)
    {
        ItemConfigBase itemConfig = ResSystem.LoadAsset<ItemConfigBase>(itemId);
        if (bagData.money < itemConfig.price)
        {
            UISystem.Show<UI_MessagePopUp>().ShowMessage(LocalizationKey.moneyLack, Color.yellow);
            return;
        }
        int bagIndex = -1;
        bagData.TryGetItemLayPos(itemId, out bagIndex);
        if (bagIndex == -1)
        {
            UISystem.Show<UI_MessagePopUp>().ShowMessage(LocalizationKey.bagSpaceLack, Color.yellow);
            return;
        }
        NetMessageManager.Instance.SendMessageToServer<C2S_ShopBuyItem>(NetMessageType.C2S_ShopBuyItem, new C2S_ShopBuyItem
        {
            itemId = itemId,
            bagIndex = bagIndex
        });
    }

    public void ShopSellItem(int index)
    {
        NetMessageManager.Instance.SendMessageToServer<C2S_ShopSellItem>(NetMessageType.C2S_ShopSellItem, new C2S_ShopSellItem
        {
            index = index
        });
    }

    public void UpdateOpenShop()
    {
        if (requestShopWindow)
        {
            MerchantConfig config = ResSystem.LoadAsset<MerchantConfig>(currentMerchantConfig);
            if (!ClientUtility.UIWindowExist<UI_ShopWindow>())
            {
                UISystem.Show<UI_ShopWindow>().Show(config);
                if (!ClientUtility.UIWindowExist<UI_BagWindow>())
                {
                    requestBagWindow = true;
                    RequestBagData();
                } // 同时打开Shop和Bag
            }
            else
            {
                UISystem.Close<UI_ShopWindow>();
                UISystem.Close<UI_BagWindow>();
            }
            requestShopWindow = false;
        }
    }
    #endregion

    #region 背包
    private void RequestBagData()
    {
        NetMessageManager.Instance.SendMessageToServer<C2S_GetBagData>(NetMessageType.C2S_GetBagData, new C2S_GetBagData
        {
            version = bagData == null ? -1 : bagData.version
        });
    }

    public void UseItem(int index)
    {
        if (bagData.itemDataList[index] is MaterialData) return;
        NetMessageManager.Instance.SendMessageToServer<C2S_UseItem>(NetMessageType.C2S_UseItem, new C2S_UseItem
        {
            index = index
        });
    }
    private void UpdateOpenBag()
    {
        //  打开背包
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (!ClientUtility.UIWindowExist<UI_BagWindow>())
            {
                requestBagWindow = true;
                RequestBagData();
            }
            else
            {
                UISystem.Close<UI_BagWindow>();
                UISystem.Close<UI_ShopWindow>();
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            for (int i = 0; i < bagData.itemIndexInShortCut.Count; ++i)
            {
                print($"{i} -> {bagData.itemIndexInShortCut[i]}");
            }
        }
    }
    #endregion

    #region 快捷栏
    private void ShowShortCutBat()
    {
        RequestBagData();
    }

    private void UseShortCutItem(int shortCutIndex)
    {
        int index = bagData.itemIndexInShortCut[shortCutIndex];
        if (index == -1 || bagData.itemDataList[index] is MaterialData) return;
        UseItem(index);
    }

    private void CloseShortCutBar()
    {
        UISystem.Close<UI_ShortCutBarWindow>();
    }

    private void UpdateHandleShortCutInput()
    {
        if (UISystem.GetWindow<UI_ShortCutBarWindow>())
        {
            for (int i = 1; i <= GlobalUtility.shortCutNum; ++i)
            {
                if (Input.GetKeyDown(KeyCode.Alpha0 + i))
                {
                    UseShortCutItem(i - 1);
                }
            }
        }
    }
    #endregion

    private void OnPlayerSpawn(PlayerSpawnEvent playerSpawnEvent)
    {
        if (!playerSpawnEvent.mainPlayerController.NetworkObject.IsOwner) return;
        playerController = playerSpawnEvent.mainPlayerController;
        playerController.Init();
        playerClientController = (PlayerClientController)playerController.sideController;
        //if (!playerController.TryGetComponent<PlayerClientController>(out playerClientController)) playerClientController = playerController.gameObject.AddComponent<PlayerClientController>();
        playerClientController.Init(playerController);
        cinemachineFreeLook.transform.position = playerController.transform.position;
        cinemachineFreeLook.Follow = playerClientController.camaraFollow;
        cinemachineFreeLook.LookAt = playerClientController.cameraLookPos;

        UISystem.Show<UI_PlayerInfoWindow>();
        playerClientController.MainController_UpdatePlayerHp(playerController.currentHp.Value, playerController.currentHp.Value);
    }

    private GameObject GetWeapon(string WeaponId)
    {
        if (WeaponId == "") return null;
        GameObject weaponObj = PoolSystem.GetGameObject(WeaponId);
        if(weaponObj == null)
        {
            WeaponConfig weaponConfig = ResSystem.LoadAsset<WeaponConfig>(WeaponId);
            weaponObj = Instantiate(weaponConfig.prefab);
            weaponObj.name = WeaponId;
        }
        return weaponObj;
    }

    public bool IsCompeleted()
    {
        return playerController != null;
    }
}

