using Unity.Netcode;

public enum NetMessageType : byte // 每次添加新的Type过后，需要在NetMessageManager中给ReceiveMessage加上对应case处理该类型
{
    None,
    C2S_Register,
    S2C_Register,
    C2S_Login,
    S2C_Login,
    C2S_EnterGame,
    C2S_Disconnect,
    S2C_Disconnect,
    C2S_Chat,
    S2C_Chat
}

public enum NetMessageErrorCode
{
    None,
    AccountFormat,
    NameDuplicaiton,
    NameOrPassword,
    AccountRepeatLogin
}

public struct C2S_Chat : INetworkSerializable
{
    public string info;
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref info);
    }
}
public struct S2C_Chat : INetworkSerializable
{
    public NetMessageErrorCode errorType;
    public string name;
    public string info;
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue<NetMessageErrorCode>(ref errorType);
        serializer.SerializeValue(ref name);
        serializer.SerializeValue(ref info);
    }
}
public struct C2S_Disconnect : INetworkSerializable
{
    public NetMessageErrorCode errorType;
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue<NetMessageErrorCode>(ref errorType);
    }
}
public struct S2C_Disconnect : INetworkSerializable
{
    public NetMessageErrorCode errorType;
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue<NetMessageErrorCode>(ref errorType);
    }
}

public struct C2S_EnterGame : INetworkSerializable
{
    public NetMessageErrorCode errorType;
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue<NetMessageErrorCode>(ref errorType);
    }
}

public struct S2C_Login : INetworkSerializable
{
    public NetMessageErrorCode errorType;
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue<NetMessageErrorCode>(ref errorType);
    }
}
public struct S2C_Register : INetworkSerializable
{
    public NetMessageErrorCode errorType;
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue<NetMessageErrorCode>(ref errorType);
    }
}

public struct C2S_Register : INetworkSerializable
{
    public AccountInfo accountInfo;
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        accountInfo.NetworkSerialize(serializer);
    }
}
public struct C2S_Login : INetworkSerializable
{
    public AccountInfo accountInfo;
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        accountInfo.NetworkSerialize(serializer);
    }
}

public struct AccountInfo : INetworkSerializable
{
    public string playerName;
    public string password;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref playerName);
        serializer.SerializeValue(ref password);
    }
} 