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
    S2C_Chat,
    C2S_GetBagData,
    S2C_GetBagData,
    C2S_UseItem,
    S2C_UpdateBagData
}

public enum NetMessageErrorCode
{
    None,
    AccountFormat,
    NameDuplicaiton,
    NameOrPassword,
    AccountRepeatLogin
}

public struct C2S_UseItem : INetworkSerializable
{
    public int index;
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue<int>(ref index);
    }
}
public struct S2C_UpdateBagData : INetworkSerializable
{
    public ItemDataBase itemData;
    public ItemType itemType;
    public int index;
    public int version;
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue<ItemType>(ref itemType);
        serializer.SerializeValue<int>(ref index);
        serializer.SerializeValue<int>(ref version);
        if (serializer.IsReader) 
        {
            switch (itemType)
            {
                case ItemType.Weapon:
                    itemData = new WeaponData();
                    break;
                case ItemType.Consumable:
                    itemData = new ConsumableData();
                    break;
                case ItemType.Material:
                    itemData = new MaterialData();
                    break;
            }
        }
        if (itemData != null) itemData.NetworkSerialize(serializer);
    }
}
public struct C2S_GetBagData : INetworkSerializable
{
    public int version;
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue<int>(ref version);
    }
}
public struct S2C_GetBagData : INetworkSerializable
{
    public bool haveBag;
    public BagData bagData;
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue<bool>(ref haveBag);

        if (!haveBag) return;
        if (serializer.IsReader)
        {
            bagData = new BagData();
            bagData.NetworkSerialize(serializer);
        }
        if (serializer.IsWriter)
        {
            bagData.NetworkSerialize(serializer);
        }
    }
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