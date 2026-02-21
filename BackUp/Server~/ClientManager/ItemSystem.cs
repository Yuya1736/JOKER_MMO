using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using System;
using Unity.Netcode;
using UnityEngine;

public partial class ClientsManager
{
    public void InitItemSystem()
    {
        NetMessageManager.Instance.RegisterOnReceiveMessageCallback(NetMessageType.C2S_GetBagData, OnReceiveGetBagDataMessage);
        NetMessageManager.Instance.RegisterOnReceiveMessageCallback(NetMessageType.C2S_UseItem, OnReceiveUseItemMessage);
        NetMessageManager.Instance.RegisterOnReceiveMessageCallback(NetMessageType.C2S_BagExchangeItem, OnReceiveBagExchangeItemMessage);
        NetMessageManager.Instance.RegisterOnReceiveMessageCallback(NetMessageType.C2S_ChangeShortCutIndex, OnReceiveChangeShortCutIndex);
        NetMessageManager.Instance.RegisterOnReceiveMessageCallback(NetMessageType.C2S_ExchangeShortCut, OnReceiveExchangeShortCut);
        NetMessageManager.Instance.RegisterOnReceiveMessageCallback(NetMessageType.C2S_ShopBuyItem, OnReceiveShopBuyItem);
        NetMessageManager.Instance.RegisterOnReceiveMessageCallback(NetMessageType.C2S_ShopSellItem, OnReceiveShopSellItem);
        NetMessageManager.Instance.RegisterOnReceiveMessageCallback(NetMessageType.C2S_CraftItem, OnReceiveCraftItem);
    }

    private void OnReceiveCraftItem(ulong clientId, INetworkSerializable serializable)
    {
        C2S_CraftItem message = (C2S_CraftItem)serializable;
        Client client = clientIdDic[clientId];
        PlayerData playerData = client.playerData;
        ItemConfigBase itemConfig = ServerResSystem.GetItemConfig(message.itemId);
        if (itemConfig != null)
        {
            // 检测玩家是否拥有足够的材料
            foreach (var itemPair in itemConfig.craftItemDic) 
            {
                if (!playerData.bagData.CheckHasItem(itemPair.Key, itemPair.Value, out int index)) return;
            }

            // 扣除材料
            foreach (var itemPair in itemConfig.craftItemDic)
            {
                if (playerData.bagData.CheckHasItem(itemPair.Key, itemPair.Value, out int index))
                {
                    playerData.bagData.RemoveItem(index, itemPair.Value);
                    NetMessageManager.Instance.SendMessageToClient<S2C_BagUpdateItem>(clientId, NetMessageType.S2C_BagUpdateItem, new S2C_BagUpdateItem
                    {
                        index = index,
                        isUse = false,
                        itemData = playerData.bagData.itemDataList[index],
                        itemType = playerData.bagData.itemDataList[index] != null ? playerData.bagData.itemDataList[index].GetItemType() : ItemType.Empty,
                        version = playerData.bagData.version
                    });
                }
            }

            ItemDataBase itemDataBase = itemConfig.GetDefaultItemData();
            if (itemDataBase is StackableItemDataBase)
            {
                if (playerData.bagData.itemDataList[message.bagIndex] == null) // 如果背包格子没有物品，直接放入合成的物品
                {
                    playerData.bagData.itemDataList[message.bagIndex] = itemDataBase;
                }
                else if (playerData.bagData.itemDataList[message.bagIndex].id == itemDataBase.id) // 如果背包格子里的物品和合成的物品是同一种，叠加数量
                    ((StackableItemDataBase)playerData.bagData.itemDataList[message.bagIndex]).count += message.count;
            }
            else
            {
                playerData.bagData.itemDataList[message.bagIndex] = itemDataBase;
            }
            playerData.bagData.AddVersion();
            NetMessageManager.Instance.SendMessageToClient<S2C_BagUpdateItem>(clientId, NetMessageType.S2C_BagUpdateItem, new S2C_BagUpdateItem
            {
                index = message.bagIndex,
                isUse = false,
                itemData = playerData.bagData.itemDataList[message.bagIndex],
                itemType = playerData.bagData.itemDataList[message.bagIndex] != null ? playerData.bagData.itemDataList[message.bagIndex].GetItemType() : ItemType.Empty,
                version = playerData.bagData.version
            });
        }
    }

    private void OnReceiveShopSellItem(ulong clientId, INetworkSerializable serializable)
    {
        C2S_ShopSellItem message = (C2S_ShopSellItem)serializable;
        Client client = clientIdDic[clientId];
        PlayerData playerData = client.playerData;
        if (playerData.bagData.itemDataList[message.index] != null)
        {
            ItemDataBase itemData = playerData.bagData.itemDataList[message.index];
            ((StackableItemDataBase)itemData).count -= 1;
            if (itemData is StackableItemDataBase)
            {
                playerData.bagData.money += ServerResSystem.GetItemConfig(playerData.bagData.itemDataList[message.index].id).price;
                if (((StackableItemDataBase)itemData).count > 0)
                {
                    playerData.bagData.AddVersion();
                    NetMessageManager.Instance.SendMessageToClient<S2C_BagUpdateItem>(clientId, NetMessageType.S2C_BagUpdateItem, new S2C_BagUpdateItem
                    {
                        itemData = itemData,
                        index = message.index,
                        isUse = false,
                        itemType = itemData.GetItemType(),
                        version = playerData.bagData.version
                    });
                }
                else
                {
                    playerData.bagData.itemDataList[message.index] = null;
                    playerData.bagData.AddVersion();
                }
            }
            else
            {
                playerData.bagData.itemDataList[message.index] = null;
                playerData.bagData.AddVersion();
            }
            NetMessageManager.Instance.SendMessageToClient<S2C_BagUpdateItem>(clientId, NetMessageType.S2C_BagUpdateItem, new S2C_BagUpdateItem
            {
                itemData = null,
                index = message.index,
                isUse = false,
                itemType = ItemType.Empty,
                version = playerData.bagData.version
            });
            NetMessageManager.Instance.SendMessageToClient<S2C_BagUpdateMoney>(clientId, NetMessageType.S2C_BagUpdateMoney, new S2C_BagUpdateMoney
            {
                money = playerData.bagData.money
            });
        }
    }

    private void OnReceiveShopBuyItem(ulong clientId, INetworkSerializable serializable)
    {
        C2S_ShopBuyItem message = (C2S_ShopBuyItem)serializable;
        Client client = clientIdDic[clientId];
        PlayerData playerData = client.playerData;
        if (playerData.bagData.money >= ServerResSystem.GetItemConfig(message.itemId).price)
        {
            if (playerData.bagData.itemDataList[message.bagIndex] == null)
            {
                playerData.bagData.money -= ServerResSystem.GetItemConfig(message.itemId).price;
                playerData.bagData.itemDataList[message.bagIndex] = ServerResSystem.GetItemConfig(message.itemId).GetDefaultItemData();
                playerData.bagData.AddVersion();
            }
            else if (playerData.bagData.itemDataList[message.bagIndex] is StackableItemDataBase)
            {
                playerData.bagData.money -= ServerResSystem.GetItemConfig(message.itemId).price;
                ((StackableItemDataBase)playerData.bagData.itemDataList[message.bagIndex]).count += 1;
                playerData.bagData.AddVersion();
            }
            NetMessageManager.Instance.SendMessageToClient<S2C_BagUpdateItem>(clientId, NetMessageType.S2C_BagUpdateItem, new S2C_BagUpdateItem
            {
                itemData = playerData.bagData.itemDataList[message.bagIndex],
                index = message.bagIndex,
                isUse = false,
                itemType = playerData.bagData.itemDataList[message.bagIndex].GetItemType(),
                version = playerData.bagData.version
            });
            NetMessageManager.Instance.SendMessageToClient<S2C_BagUpdateMoney>(clientId, NetMessageType.S2C_BagUpdateMoney, new S2C_BagUpdateMoney
            {
                money = playerData.bagData.money
            });    
        }
    }

    private void OnReceiveExchangeShortCut(ulong clientId, INetworkSerializable serializable)
    {
        C2S_ExchangeShortCut message = (C2S_ExchangeShortCut)serializable;
        Client client = clientIdDic[clientId];
        PlayerData playerData = client.playerData;
        int itemIndex1 = playerData.bagData.itemIndexInShortCut[message.shortCutIndex1];
        int itemIndex2 = playerData.bagData.itemIndexInShortCut[message.shortCutIndex2];
        playerData.bagData.ExchangeShortCut(message.shortCutIndex1, message.shortCutIndex2);
        NetMessageManager.Instance.SendMessageToClient<S2C_ChangeShortCutIndex>(clientId, NetMessageType.S2C_ChangeShortCutIndex, new S2C_ChangeShortCutIndex
        {
            itemIndex = itemIndex1,
            shortCutIndex = message.shortCutIndex2
        });
        NetMessageManager.Instance.SendMessageToClient<S2C_ChangeShortCutIndex>(clientId, NetMessageType.S2C_ChangeShortCutIndex, new S2C_ChangeShortCutIndex
        {
            itemIndex = itemIndex2,
            shortCutIndex = message.shortCutIndex1
        });

    }   

    private void OnReceiveChangeShortCutIndex(ulong clientId, INetworkSerializable serializable)
    {
        C2S_ChangeShortCutIndex message = (C2S_ChangeShortCutIndex)serializable;
        Client client = clientIdDic[clientId];
        PlayerData playerData = client.playerData;
        int oldItemShortCut = playerData.bagData.GetShortCutBarIndex(message.itemIndex);
        int oldItemIndex = playerData.bagData.itemIndexInShortCut[message.shortCutIndex];
        if (oldItemShortCut != -1)
        {
            playerData.bagData.SetShortCut(oldItemShortCut, oldItemIndex);
            NetMessageManager.Instance.SendMessageToClient<S2C_ChangeShortCutIndex>(clientId, NetMessageType.S2C_ChangeShortCutIndex, new S2C_ChangeShortCutIndex
            {
                itemIndex = oldItemIndex,
                shortCutIndex = oldItemShortCut
            });
        }
        playerData.bagData.SetShortCut(message.shortCutIndex, message.itemIndex);
        NetMessageManager.Instance.SendMessageToClient<S2C_ChangeShortCutIndex>(clientId, NetMessageType.S2C_ChangeShortCutIndex, new S2C_ChangeShortCutIndex
        {
            itemIndex = message.itemIndex,
            shortCutIndex = message.shortCutIndex
        });
    }

    private void OnReceiveBagExchangeItemMessage(ulong clientId, INetworkSerializable serializable)
    {
#if UNITY_EDITOR || UNITY_SERVER
        C2S_BagExchangeItem message = (C2S_BagExchangeItem)serializable;
        Client client = clientIdDic[clientId];
        PlayerData playerData = client.playerData;
        playerData.bagData.ExchangeItem(message.oldIndex, message.newIndex); // 服务端背包数据变更
        NetMessageManager.Instance.SendMessageToClient<S2C_BagExchangeItem>(clientId, NetMessageType.S2C_BagExchangeItem, new S2C_BagExchangeItem
        {
            oldIndex = message.oldIndex,
            newIndex = message.newIndex
        });
        //NetMessageManager.Instance.SendMessageToClient<S2C_BagUpdateItem>(clientId, NetMessageType.S2C_BagUpdateItem, new S2C_BagUpdateItem
        //{
        //    index = message.oldIndex,
        //    itemData = playerData.bagData.itemDataList[message.oldIndex],
        //    itemType = playerData.bagData.itemDataList[message.oldIndex].GetItemType(),
        //    oldIndex = message.newIndex,
        //    version = playerData.bagData.version
        //}); // 客户端oldIndex物品更新
        //playerData.bagData.AddVersion();
        //NetMessageManager.Instance.SendMessageToClient<S2C_BagUpdateItem>(clientId, NetMessageType.S2C_BagUpdateItem, new S2C_BagUpdateItem
        //{
        //    index = message.newIndex,
        //    itemData = playerData.bagData.itemDataList[message.newIndex],
        //    itemType = playerData.bagData.itemDataList[message.newIndex].GetItemType(),
        //    oldIndex = message.oldIndex,
        //    version = playerData.bagData.version
        //}); // 客户端newIndex物品更新
        // 处理快捷栏与背包物品的对应关系
        int shortCutIndex1 = playerData.bagData.GetShortCutBarIndex(message.oldIndex);
        int shortCutIndex2 = playerData.bagData.GetShortCutBarIndex(message.newIndex);
        if (shortCutIndex1 != -1)
        {
            NetMessageManager.Instance.SendMessageToClient<S2C_ChangeShortCutIndex>(clientId, NetMessageType.S2C_ChangeShortCutIndex, new S2C_ChangeShortCutIndex
            {
                shortCutIndex = shortCutIndex1,
                itemIndex = message.newIndex
            });
            playerData.bagData.itemIndexInShortCut[shortCutIndex1] = message.newIndex;
        }
        if (shortCutIndex2 != -1)
        {
            NetMessageManager.Instance.SendMessageToClient<S2C_ChangeShortCutIndex>(clientId, NetMessageType.S2C_ChangeShortCutIndex, new S2C_ChangeShortCutIndex
            {
                shortCutIndex = shortCutIndex2,
                itemIndex = message.oldIndex
            });
            playerData.bagData.itemIndexInShortCut[shortCutIndex2] = message.oldIndex;
        }
#endif
    }

    private void OnReceiveUseItemMessage(ulong clientId, INetworkSerializable serializable)
    {
#if UNITY_EDITOR || UNITY_SERVER // 因为bagData.UseItem是添加了服务端宏， 如果调用时不添加服务端宏，编译会不通过（Unity不会剥离程序集）
        C2S_UseItem message = (C2S_UseItem)serializable;
        Client client = clientIdDic[clientId];
        PlayerData playerData = client.playerData;
        S2C_BagUpdateItem result = new S2C_BagUpdateItem()
        {
            oldIndex = playerData.bagData.usedWeponIndex,
            index = message.index,
            isUse = true,
            itemType = ItemType.Empty,
            itemData = null,
            version = -1,
        };
        if (playerData != null && playerData.bagData != null)
        {
            ItemDataBase itemData = playerData.bagData.UseItem(message.index);
            result.itemData = itemData;
            result.version = playerData.bagData.version;
            if (itemData != null) result.itemType = itemData.GetItemType();
            if (itemData is WeaponData) // 如果是武器
            {
                playerData.bagData.usedWeponIndex = message.index; // 切换bagData的usedWeponIndex
                if (client.playerController != null)
                {
                    client.playerController.currentWeapon.Value = playerData.bagData.itemDataList[message.index].id; // 将PlayerController中的武器网络变量修改为当前武器
                }
            }
        }
        NetMessageManager.Instance.SendMessageToClient<S2C_BagUpdateItem>(clientId, NetMessageType.S2C_BagUpdateItem, result);
#endif
    }

    private void OnReceiveGetBagDataMessage(ulong clientId, INetworkSerializable serializable)
    {
        C2S_GetBagData c2S_GetBagInfo = (C2S_GetBagData)serializable;
        Client client = clientIdDic[clientId];
        S2C_GetBagData s2C_GetBagDataInfo = new S2C_GetBagData { haveBag = false };
        if (client.playerData.bagData.version != c2S_GetBagInfo.version)
        {
            s2C_GetBagDataInfo.haveBag = true;
            s2C_GetBagDataInfo.bagData = client.playerData.bagData;
            s2C_GetBagDataInfo.bagData.money = ServerResSystem.serverConfig.defaultMoney;

            s2C_GetBagDataInfo.bagData.itemDataList[0] = new WeaponData() { id = ItemConfigKey.weapon0 };
            s2C_GetBagDataInfo.bagData.itemDataList[1] = new WeaponData() { id = ItemConfigKey.weapon1 };
            s2C_GetBagDataInfo.bagData.itemDataList[2] = new MaterialData() { id = ItemConfigKey.material0, count = 11 };
            s2C_GetBagDataInfo.bagData.itemDataList[3] = new MaterialData() { id = ItemConfigKey.material1, count = 22 };
            s2C_GetBagDataInfo.bagData.itemDataList[4] = new MaterialData() { id = ItemConfigKey.material2, count = 33 };
            s2C_GetBagDataInfo.bagData.itemDataList[5] = new MaterialData() { id = ItemConfigKey.material3, count = 44 };
            s2C_GetBagDataInfo.bagData.itemDataList[6] = new ConsumableData() { id = ItemConfigKey.consumable0, count = 1 };
            s2C_GetBagDataInfo.bagData.itemDataList[7] = new ConsumableData() { id = ItemConfigKey.consumable1, count = 2 };
            s2C_GetBagDataInfo.bagData.itemDataList[8] = new ConsumableData() { id = ItemConfigKey.consumable2, count = 3 };
            s2C_GetBagDataInfo.bagData.itemDataList[9] = new ConsumableData() { id = ItemConfigKey.consumable3, count = 4 };
            s2C_GetBagDataInfo.bagData.itemDataList[10] = new ConsumableData() { id = ItemConfigKey.consumable4, count = 5 };

            s2C_GetBagDataInfo.bagData.itemIndexInShortCut[0] = 0;
            s2C_GetBagDataInfo.bagData.itemIndexInShortCut[1] = 1;
            s2C_GetBagDataInfo.bagData.itemIndexInShortCut[2] = 2;
            s2C_GetBagDataInfo.bagData.itemIndexInShortCut[3] = 3;
            s2C_GetBagDataInfo.bagData.itemIndexInShortCut[4] = 4;
            s2C_GetBagDataInfo.bagData.itemIndexInShortCut[5] = 5;
            s2C_GetBagDataInfo.bagData.itemIndexInShortCut[6] = 6;
            s2C_GetBagDataInfo.bagData.itemIndexInShortCut[7] = -1;
        }
        NetMessageManager.Instance.SendMessageToClient<S2C_GetBagData>(clientId, NetMessageType.S2C_GetBagData, s2C_GetBagDataInfo);
    }
}
