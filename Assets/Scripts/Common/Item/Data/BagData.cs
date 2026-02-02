using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using Unity.Netcode;

public class BagData : INetworkSerializable
{
    public const int maxItemCount = 30;
    public List<ItemDataBase> itemDataList = new List<ItemDataBase>(maxItemCount);
    [BsonIgnore]
    public int version;
    public int usedWeponIndex;

    public BagData()
    {
        for (int i = 0; i < maxItemCount; ++i) itemDataList.Add(null);
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        for(int i = 0;i < maxItemCount; ++i)
        {
            if(serializer.IsReader)
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
                if(itemType != ItemType.Empty) itemData.NetworkSerialize(serializer);
            }
        }
    }

    #region 服务端
#if UNITY_SERVER || UNITY_EDITOR
    public ItemDataBase UseItem(int index)
    {
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

    public ItemDataBase TryGetItem(string id, out int index)
    {
        for (int i = 0; i < itemDataList.Count; ++i) 
        {
            ItemDataBase itemData = itemDataList[i];
            if(itemData !=  null && itemData.id == id)
            {
                index = i;
                return itemData;
            }
        }
        index = -1;
        return null;
    }

    public void RemoveItem(int index)
    {
        itemDataList[index] = null;
    }

    public bool TryGetEmptyIndex(out int index)
    {
        for (int i = 0; i < itemDataList.Count; ++i)
        {
            ItemDataBase itemData = itemDataList[i];
            if(itemData == null)
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
        bool ok = TryGetEmptyIndex(out index);
        if(ok)
        {
            itemDataList[index] = new WeaponData() { id = id };
            return true;
        }
        return false;
    }

    public bool TryAddStackableItem<T>(string id, int count, out int index) where T : StackableItemDataBase, new()
    {
        for (int i = 0; i < itemDataList.Count; ++i)
        {
            ItemDataBase itemData = itemDataList[i];
            if(itemData != null && itemData.id == id)
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
            T newItem = new T() { id = id , count = count};
            itemDataList[index] = newItem;
            return true;
        }
        return false;
    }
#endif
    #endregion
}
