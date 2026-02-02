using Unity.Netcode;
using UnityEngine;

public partial class ClientsManager
{
    public void InitItemSystem()
    {
        NetMessageManager.Instance.RegisterOnReceiveMessageCallback(NetMessageType.C2S_GetBagData, OnReceiveGetBagDataMessage);
        NetMessageManager.Instance.RegisterOnReceiveMessageCallback(NetMessageType.C2S_UseItem, OnReceiveUseItemMessage);
    }

    private void OnReceiveUseItemMessage(ulong clientId, INetworkSerializable serializable)
    {
#if UNITY_EDITOR || UNITY_SERVER // 因为bagData.UseItem是添加了服务端宏， 如果调用时不添加服务端宏，编译会不通过（Unity不会剥离程序集）
        C2S_UseItem message = (C2S_UseItem)serializable;
        Client client = clientIdDic[clientId];
        PlayerData playerData = client.playerData;
        S2C_UpdateBagData result = new S2C_UpdateBagData()
        {
            oldIndex = playerData.bagData.usedWeponIndex,
            index = message.index,
            itemType = ItemType.Empty,
            itemData = null,
            version = -1
        };
        if (playerData != null && playerData.bagData != null)
        {
            ItemDataBase itemData = playerData.bagData.UseItem(message.index);
            result.itemData = itemData;
            result.version = ++playerData.bagData.version;
            if (itemData != null) result.itemType = itemData.GetItemType();
            if (itemData is WeaponData) // 如果是武器
            {
                playerData.bagData.usedWeponIndex = message.index; // 切换bagData的usedWeponIndex
                if (client.playerController != null)
                {
                    print(playerData.bagData.itemDataList[message.index].id);
                    client.playerController.currentWeapon.Value = playerData.bagData.itemDataList[message.index].id; // 将PlayerController中的武器网络变量修改为当前武器
                }
            }
        }
        NetMessageManager.Instance.SendMessageToClient<S2C_UpdateBagData>(clientId, NetMessageType.S2C_UpdateBagData, result);
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
        }
        NetMessageManager.Instance.SendMessageToClient<S2C_GetBagData>(clientId, NetMessageType.S2C_GetBagData, s2C_GetBagDataInfo);
    }
}
