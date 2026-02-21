using JKFrame;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using Unity.Netcode;

public class BagData : INetworkSerializable
{
    [BsonIgnore]
    public static int maxItemCount => GlobalUtility.bagMaxItemCount;
    public List<ItemDataBase> itemDataList = new List<ItemDataBase>(maxItemCount);
    public List<int> itemIndexInShortCut = new List<int>(GlobalUtility.shortCutNum);
    [BsonIgnore]
    public int version;
    public int usedWeponIndex;
    public int money;

    public BagData()
    {
        for (int i = 0; i < maxItemCount; ++i) itemDataList.Add(null);
        for (int i = 0; i < GlobalUtility.shortCutNum; ++i) itemIndexInShortCut.Add(-1);
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue<int>(ref usedWeponIndex);
        serializer.SerializeValue<int>(ref money);
        for (int i = 0; i < GlobalUtility.shortCutNum; ++i)
        {
            if (serializer.IsReader)
            {
                int index = -1;
                serializer.SerializeValue<int>(ref index);
                itemIndexInShortCut[i] = index;
            }
            if (serializer.IsWriter)
            {
                int index = itemIndexInShortCut[i];
                serializer.SerializeValue<int>(ref index);
            }
        }
        for (int i = 0; i < maxItemCount; ++i)
        {
            if (serializer.IsReader)
            {
                FastBufferReader reader = serializer.GetFastBufferReader();
                reader.ReadValueSafe<ItemType>(out ItemType itemType);
                switch (itemType)
                {
                    case ItemType.Empty:
                        itemDataList[i] = null;
                        break;
                    case ItemType.Weapon:
                        WeaponData weaponData = new WeaponData();
                        weaponData.NetworkSerialize(serializer);
                        itemDataList[i] = weaponData;
                        break;
                    case ItemType.Consumable:
                        ConsumableData consumableData = new ConsumableData();
                        consumableData.NetworkSerialize(serializer);
                        itemDataList[i] = consumableData;
                        break;
                    case ItemType.Material:
                        MaterialData materialData = new MaterialData();
                        materialData.NetworkSerialize(serializer);
                        itemDataList[i] = materialData;
                        break;
                }
            }
            if (serializer.IsWriter)
            {
                FastBufferWriter writer = serializer.GetFastBufferWriter();
                ItemDataBase itemData = itemDataList[i];
                ItemType itemType = ItemType.Empty;
                if (itemData is null) itemType = ItemType.Empty;
                else if (itemData is WeaponData) itemType = ItemType.Weapon;
                else if (itemData is ConsumableData) itemType = ItemType.Consumable;
                else if (itemData is MaterialData) itemType = ItemType.Material;

                writer.WriteValueSafe<ItemType>(itemType);
                if (itemType != ItemType.Empty) itemData.NetworkSerialize(serializer);
            }
        }
    }

    public void SetItem(int index, ItemDataBase itemData)
    {
        AddVersion();
        itemDataList[index] = itemData;
    }

    public void SetShortCut(int shortCutIndex, int itemIndex)
    {
        itemIndexInShortCut[shortCutIndex] = itemIndex;
    }

    public void ExchangeShortCut(int shortCutIndex1, int shortCutIndex2)
    {
        int itemIndex1 = itemIndexInShortCut[shortCutIndex1];
        int itemIndex2 = itemIndexInShortCut[shortCutIndex2];
        itemIndexInShortCut[shortCutIndex1] = itemIndex2;
        itemIndexInShortCut[shortCutIndex2] = itemIndex1;
    }

//    #region 服务端
//#if UNITY_SERVER || UNITY_EDITOR
    public ItemDataBase UseItem(int index)
    {
        AddVersion();
        ItemDataBase itemData = itemDataList[index];
        if (itemData is WeaponData)
        {
            WeaponData weaponData = (WeaponData)itemData;
            weaponData.isUsed = true;
            itemDataList[index] = weaponData;
            return weaponData;
        }
        else if (itemData is ConsumableData)
        {
            ConsumableData consumableData = (ConsumableData)itemData;
            consumableData.count--;
            itemDataList[index] = consumableData;
            if (consumableData.count <= 0)
            {
                itemDataList[index] = null;
                consumableData = null;
            }
            return consumableData;
        }
        else return null;
    }

    public bool TryGetItemLayPos(string id, out int index)
    {
        ItemConfigBase itemConfig = ResSystem.LoadAsset<ItemConfigBase>(id);
        index = -1;
        if (itemConfig.GetDefaultItemData(false) is StackableItemDataBase) // 如果是可堆叠物品 检查背包是否有相同物品
        {
            TryGetItem(id, out index);
        }
        if (index == -1) TryGetEmptyIndex(out index);
        if (index == -1) return false;
        return true;
    }

    public bool CheckHasItem(string id, int count, out int index)
    {
        // 目前没有处理需要多个武器的逻辑
        ItemDataBase itemDataBase = TryGetItem(id, out index);
        if (itemDataBase == null) return false;
        if (itemDataBase is StackableItemDataBase && ((StackableItemDataBase)itemDataBase).count < count)
        {
            index = -1;
            return false;
        }
        return true;
    }

    public ItemDataBase TryGetItem(string id, out int index)
    {
        for (int i = 0; i < itemDataList.Count; ++i)
        {
            ItemDataBase itemData = itemDataList[i];
            if (itemData != null && itemData.id == id)
            {
                index = i;
                return itemData;
            }
        }
        index = -1;
        return null;
    }

    public void RemoveItem(int index, int count)
    {
        AddVersion();
        ItemDataBase itemData = itemDataList[index];
        if (itemData is StackableItemDataBase)
        {
            StackableItemDataBase stackableItemData = itemData as StackableItemDataBase;
            stackableItemData.count -= count;
            itemDataList[index] = stackableItemData;
            if (stackableItemData.count <= 0) itemDataList[index] = null;
        }
        else itemDataList[index] = null;
    }

    public void RemoveItem(int index)
    {
        AddVersion();
        itemDataList[index] = null;
    }

    public bool TryGetEmptyIndex(out int index)
    {
        for (int i = 0; i < itemDataList.Count; ++i)
        {
            ItemDataBase itemData = itemDataList[i];
            if (itemData == null)
            {
                index = i;
                return true;
            }
        }
        index = -1;
        return false;
    }

    public bool TryAddWeapon(string id, out int index)
    {
        AddVersion();
        bool ok = TryGetEmptyIndex(out index);
        if (ok)
        {
            itemDataList[index] = new WeaponData() { id = id };
            return true;
        }
        return false;
    }

    public bool TryAddStackableItem<T>(string id, int count, out int index) where T : StackableItemDataBase, new()
    {
        AddVersion();
        for (int i = 0; i < itemDataList.Count; ++i)
        {
            ItemDataBase itemData = itemDataList[i];
            if (itemData != null && itemData.id == id)
            {
                StackableItemDataBase stackableItemData = itemData as StackableItemDataBase;
                stackableItemData.count += count;
                index = i;
                return true;
            }
        }
        bool ok = TryGetEmptyIndex(out index);
        if (ok)
        {
            T newItem = new T() { id = id, count = count };
            itemDataList[index] = newItem;
            return true;
        }
        return false;
    }

    public void ExchangeItem(int index1, int index2)
    {
        AddVersion();
        if (usedWeponIndex == index1) usedWeponIndex = index2;
        else if (usedWeponIndex == index2) usedWeponIndex = index1;

        ItemDataBase temp = itemDataList[index1];
        itemDataList[index1] = itemDataList[index2];
        itemDataList[index2] = temp;
    }

    public int GetShortCutBarIndex(int itemIndex)
    {
        for (int i = 0; i < GlobalUtility.shortCutNum; ++i)
        {
            if (itemIndexInShortCut[i] == itemIndex)
            {
                return i;
            }
        }
        return -1;
    }

    public void AddVersion()
    {
        version++;
    }

//#endif
//    #endregion
}
