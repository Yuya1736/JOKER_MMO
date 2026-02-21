using Cinemachine;
using JKFrame;
using Unity.Netcode;
using UnityEngine;

public class PlayerManager : SingletonMono<PlayerManager>
{
    public static PlayerController localPlayer;
    public static BagData bagData;
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


    public bool IsCompeleted()
    {
        return localPlayer != null;
    }

    public void Init()
    {
        EventSystem.AddTypeEventListener<LocalPlayerEvent>(OnInitLocalPlayer);
        PlayerController.SetGetWeaponFunc(GetWeapon);
        NetMessageManager.Instance.RegisterOnReceiveMessageCallback(NetMessageType.S2C_GetBagData, OnReceiveBagData);
        NetMessageManager.Instance.RegisterOnReceiveMessageCallback(NetMessageType.S2C_BagUpdateItem, OnReceiveUpdateBagData);
        NetMessageManager.Instance.RegisterOnReceiveMessageCallback(NetMessageType.S2C_ChangeShortCutIndex, OnReceiveChangeShortCutIndex);
        NetMessageManager.Instance.RegisterOnReceiveMessageCallback(NetMessageType.S2C_BagExchangeItem, OnReceiveBagExchangeItem);
        NetMessageManager.Instance.RegisterOnReceiveMessageCallback(NetMessageType.S2C_BagUpdateMoney, OnReceiveBagUpdateMoney);
        ShowShortCutBat();
    }

    private void OnDestroy()
    {
        EventSystem.RemoveTypeEventListener<LocalPlayerEvent>(OnInitLocalPlayer);
        NetMessageManager.Instance.UnRegisterOnReceiveMessageCallback(NetMessageType.S2C_GetBagData, OnReceiveBagData);
        NetMessageManager.Instance.UnRegisterOnReceiveMessageCallback(NetMessageType.S2C_BagUpdateItem, OnReceiveUpdateBagData);
        NetMessageManager.Instance.UnRegisterOnReceiveMessageCallback(NetMessageType.S2C_ChangeShortCutIndex, OnReceiveChangeShortCutIndex);
        NetMessageManager.Instance.UnRegisterOnReceiveMessageCallback(NetMessageType.S2C_BagExchangeItem, OnReceiveBagExchangeItem);
        NetMessageManager.Instance.UnRegisterOnReceiveMessageCallback(NetMessageType.S2C_BagUpdateMoney, OnReceiveBagUpdateMoney);
        CloseShortCutBar();
    }
    private void Update()
    {
        UpdateOpenBag();
        UpdateOpenShop();
        UpdateOpenCraft();
        UpdateHandleShortCutInput();
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
            for(int i = 0;i < bagData.itemIndexInShortCut.Count; ++i)
            {
                print($"{i} -> {bagData.itemIndexInShortCut[i]}");
            }
        }
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

    private void ShowShortCutBat()
    {
        RequestBagData();
    }

    private void RequestBagData()
    {
        NetMessageManager.Instance.SendMessageToServer<C2S_GetBagData>(NetMessageType.C2S_GetBagData, new C2S_GetBagData
        {
            version = bagData == null ? -1 : bagData.version
        });
    }

    public void RequestOpenShop(string merchantConfig)
    {
        currentMerchantConfig = merchantConfig;
        requestShopWindow = true;
    }

    public void RequestOpenCraft(string crafterConfig)
    {
        currentCrafterConfig = crafterConfig;
        requestCraftWindow = true;
    }

    public void UseItem(int index)
    {
        if (bagData.itemDataList[index] is MaterialData) return;
        NetMessageManager.Instance.SendMessageToServer<C2S_UseItem>(NetMessageType.C2S_UseItem, new C2S_UseItem
        {
            index = index
        });
    }

    private void UseShortCutItem(int shortCutIndex)
    {
        int index = bagData.itemIndexInShortCut[shortCutIndex];
        if (index == -1 || bagData.itemDataList[index] is MaterialData) return;
        UseItem(index);
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

    private void CloseShortCutBar()
    {
        UISystem.Close<UI_ShortCutBarWindow>();
    }

    private void OnInitLocalPlayer(LocalPlayerEvent localPlayerEvent)
    {
        localPlayer = localPlayerEvent.localPlayer;
        
        cinemachineFreeLook.transform.position = localPlayer.transform.position;
        cinemachineFreeLook.Follow = localPlayer.camaraFollow;
        cinemachineFreeLook.LookAt = localPlayer.cameraLookPos;
    }

    private GameObject GetWeapon(string WeaponId)
    {
        GameObject weaponObj = PoolSystem.GetGameObject(WeaponId);
        if(weaponObj == null)
        {
            WeaponConfig weaponConfig = ResSystem.LoadAsset<WeaponConfig>(WeaponId);
            weaponObj = Instantiate(weaponConfig.prefab);
            weaponObj.name = WeaponId;
        }
        return weaponObj;
    }

}
