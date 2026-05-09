using System.Collections.Generic;
using Unity.Netcode;

public class TaskDatas : INetworkSerializable
{
    public List<TaskData> taskDataList = new List<TaskData>();
    public int version;
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        int count = taskDataList.Count;
        serializer.SerializeValue(ref version);
        serializer.SerializeValue(ref count);
        if (serializer.IsReader)
        {
            for (int i = 0; i < count; i++)
            {
                TaskData taskData = new TaskData();
                serializer.SerializeValue(ref taskData);
                taskDataList.Add(taskData);
            }
        }
        if (serializer.IsWriter)
        {
            for (int i = 0; i < count; i++)
            {
                TaskData taskData = taskDataList[i];
                serializer.SerializeValue(ref taskData);
            }
        }
    }

    public void AddNextTask(TaskConfig taskConfig)
    {
        if (taskConfig == null || taskConfig.nextTaskId == null || taskConfig.nextTaskId == "") return;
        AddTask(new TaskData() { taskConfigId = taskConfig.nextTaskId, progress = 0 });
    }

    public void UpdateTaskProgress(int index, int progress)
    {
        taskDataList[index].progress = progress;
        AddDataVersion();
    }

    public void AddTask(TaskData taskData)
    {
        taskDataList.Add(taskData);
        AddDataVersion();
    }

    public void CompeleteTask(int index)
    {
        taskDataList.RemoveAt(index);
        AddDataVersion();
    }

    public void AddDataVersion()
    {
        version++;
    }
}
