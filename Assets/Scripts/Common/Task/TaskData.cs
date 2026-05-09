using Unity.Netcode;

public class TaskData : INetworkSerializable
{
    public string taskConfigId;
    public int progress;
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref taskConfigId);
        serializer.SerializeValue<int>(ref progress);
    }
}
