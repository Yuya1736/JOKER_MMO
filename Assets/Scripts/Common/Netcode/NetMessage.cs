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
    S2C_BagUpdateItem,
    C2S_BagExchangeItem,
    S2C_BagExchangeItem,
    C2S_ChangeShortCutIndex,
    S2C_ChangeShortCutIndex,
    C2S_ExchangeShortCut,
    C2S_ShopBuyItem,
    S2C_BagUpdateMoney,
    C2S_ShopSellItem,
    C2S_CraftItem,
    C2S_GetTaskData,
    S2C_GetTaskData,
    C2S_CompeleteTask,
    S2C_UpdateTaskData,
    S2C_GetMoneyReward
}

public enum NetMessageErrorCode
{
    None,
    AccountFormat,
    NameDuplicaiton,
    NameOrPassword,
    AccountRepeatLogin
}

public struct S2C_GetMoneyReward : INetworkSerializable
{
    public int count;
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue<int>(ref count);
    }
}
public struct S2C_UpdateTaskData : INetworkSerializable
{
    public TaskData data;
    public int version;
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue<int>(ref version);
        if (serializer.IsReader && data == null) data = new TaskData();
        serializer.SerializeValue<TaskData>(ref data);
    }
}
public struct C2S_CompeleteTask : INetworkSerializable
{
    public int index;
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue<int>(ref index);
    }
}
public struct S2C_GetTaskData : INetworkSerializable
{
    public int version;
    public TaskDatas taskDatas;
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue<int>(ref version);
        if (serializer.IsReader && taskDatas == null) taskDatas = new TaskDatas();
        serializer.SerializeValue<TaskDatas>(ref taskDatas);
    }
}
public struct C2S_GetTaskData : INetworkSerializable
{
    public int version;
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue<int>(ref version);
    }
}
public struct C2S_CraftItem : INetworkSerializable
{
    public string itemId;
    public int count;
    public int bagIndex;
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref itemId);
        serializer.SerializeValue<int>(ref count);
        serializer.SerializeValue<int>(ref bagIndex);
    }
}

public struct C2S_ShopSellItem : INetworkSerializable
{
    public int index;
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue<int>(ref index);
    }
}

public struct S2C_BagUpdateMoney : INetworkSerializable
{
    public int money;
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue<int>(ref money);
    }
}

public struct C2S_ShopBuyItem : INetworkSerializable
{
    public string itemId;
    public int bagIndex;
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref itemId);
        serializer.SerializeValue<int>(ref bagIndex);
    }
}

public struct C2S_ExchangeShortCut : INetworkSerializable
{
    public int shortCutIndex1;
    public int shortCutIndex2;
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue<int>(ref shortCutIndex1);
        serializer.SerializeValue<int>(ref shortCutIndex2);
    }
}
public struct C2S_ChangeShortCutIndex : INetworkSerializable
{
    public int shortCutIndex;
    public int itemIndex;
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue<int>(ref shortCutIndex);
        serializer.SerializeValue<int>(ref itemIndex);
    }
}
public struct S2C_ChangeShortCutIndex : INetworkSerializable
{
    public int shortCutIndex;
    public int itemIndex;
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue<int>(ref shortCutIndex);
        serializer.SerializeValue<int>(ref itemIndex);
    }
}
public struct S2C_BagExchangeItem : INetworkSerializable
{
    public int oldIndex;
    public int newIndex;
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue<int>(ref oldIndex);
        serializer.SerializeValue<int>(ref newIndex);
    }
}
public struct C2S_BagExchangeItem : INetworkSerializable
{
    public int oldIndex;
    public int newIndex;
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue<int>(ref oldIndex);
        serializer.SerializeValue<int>(ref newIndex);
    }
}
public struct C2S_UseItem : INetworkSerializable
{
    public int index;
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue<int>(ref index);
    }
}
public struct S2C_BagUpdateItem : INetworkSerializable
{
    public ItemDataBase itemData;
    public ItemType itemType;
    public bool isUse;
    public int oldIndex; // 需要原先的武器下标，来切换SelectIcon
    public int index;
    public int version;
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue<ItemType>(ref itemType);
        serializer.SerializeValue<bool>(ref isUse);
        serializer.SerializeValue<int>(ref oldIndex);
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