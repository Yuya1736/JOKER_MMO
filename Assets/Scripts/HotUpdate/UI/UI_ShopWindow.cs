using JKFrame;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ShopWindow : UI_CustomWindowBase, IInputBlockerUI, IBagWindow
{
    [SerializeField] private Button btnClose;
    [SerializeField] private Transform itemRoot;
    private string emptySlotkey => GlobalUtility.emptySlotKey;
    private List<UI_SlotBase> slotList = new List<UI_SlotBase>(100);

    private void Awake()
    {
        btnClose.onClick.AddListener(OnBtnCloseClick);
    }

    private void OnBtnCloseClick()
    {
        UISystem.Close<UI_BagWindow>();
        UISystem.Close<UI_ShopWindow>();
    }

    public void Clear()
    {
        for (int i = slotList.Count - 1; i >= 0; i--)
        {
            slotList[i].Destroy();
        }
        slotList.Clear();
    }

    public void Show(MerchantConfig merchantConfig)
    {
        if (slotList.Count != 0) Clear();

        List<MerchantItemConfig> itemConfigList = merchantConfig.itemConfigs;
        for (int i = 0; i < GlobalUtility.bagMaxItemCount; ++i)
        {
            if (i >= itemConfigList.Count || itemConfigList[i].itemConfig == null) slotList.Add(CreateEmptySlot(i));
            else
            {
                MerchantItemConfig merchantItemConfig = itemConfigList[i];
                ItemDataBase itemData = merchantItemConfig.itemConfig.GetDefaultItemData();
                if (itemData is WeaponData)
                {
                    WeaponData weaponData = (WeaponData)itemData;
                    for(int j = 0;j < merchantItemConfig.count;++ j) slotList.Add(CreateSlot(i, weaponData));
                }
                else if (itemData is MaterialData)
                {
                    MaterialData materialData = (MaterialData)itemData;
                    materialData.count = merchantItemConfig.count;
                    UI_MaterialSlot slot = (UI_MaterialSlot)CreateSlot(i, materialData);
                    slot.HideCount();
                    slotList.Add(slot);
                }
                else if (itemData is ConsumableData)
                {
                    ConsumableData consumableData = (ConsumableData)itemData;
                    consumableData.count = merchantItemConfig.count;
                    UI_ConsumableSlot slot = (UI_ConsumableSlot)CreateSlot(i, consumableData);
                    slot.HideCount();
                    slotList.Add(slot);
                }
            }
        }
    }
    public void OnBuyItem(int index)
    {
        MerchantConfig config = ResSystem.LoadAsset<MerchantConfig>(PlayerManager.currentMerchantConfig);
        if (index >= config.itemConfigs.Count) return;
        string itemId = config.itemConfigs[index].itemConfig.name;
        PlayerManager.Instance.ShopBuyItem(itemId);
    }

    public void UpdataItem(int index, ItemDataBase itemData)
    {
        slotList[index].Destroy();
        if (itemData != null)
        {
            slotList[index] = CreateSlot(index, itemData);
            if (itemData is WeaponData)
            {
                if (PlayerManager.bagData.usedWeponIndex == slotList[index].index) ((UI_WeaponSlot)slotList[index]).SetUseState(true);
                else ((UI_WeaponSlot)slotList[index]).SetUseState(false);
            }
        }
        else
        {
            slotList[index] = CreateEmptySlot(index);
        }
    }

    public UI_SlotBase CreateEmptySlot(int index)
    {
        //UI_SlotBase emptySlot = Instantiate(ResSystem.LoadAsset<GameObject>(emptySlotkey), itemRoot).GetComponent<UI_SlotBase>();
        UI_SlotBase emptySlot = ResSystem.InstantiateGameObject<UI_SlotBase>(emptySlotkey, itemRoot);
        emptySlot.Init(this, null, null, index, null, null, null);
        emptySlot.transform.SetSiblingIndex(index);
        return emptySlot;
    }

    public UI_SlotBase CreateSlot(int index, ItemDataBase itemData)
    {
        ItemConfigBase itemConfig = ResSystem.LoadAsset<ItemConfigBase>(itemData.id);
        string slotKey = itemConfig.slotKey;
        UI_SlotBase slot = ResSystem.InstantiateGameObject<UI_SlotBase>(slotKey, itemRoot);
        slot.Init(this, itemData, itemConfig, index, OnBuyItem, null, OnDragItemToNewSlot);
        slot.transform.SetSiblingIndex(index);
        return slot;
    }

    private void OnDragItemToNewSlot(UI_SlotBase oldSlot, UI_SlotBase newSlot) // oldSlot.bagWindow is UI_BagWindow
    {
        //if (newSlot.bagWindow is UI_BagWindow) // 背包->背包
        //{
        //    NetMessageManager.Instance.SendMessageToServer<C2S_BagExchangeItem>(NetMessageType.C2S_BagExchangeItem, new C2S_BagExchangeItem
        //    {
        //        oldIndex = oldSlot.index,
        //        newIndex = newSlot.index
        //    });
        //}
        //else if (newSlot.bagWindow is UI_ShortCutBarWindow) // 背包->快捷栏
        //{
        //    NetMessageManager.Instance.SendMessageToServer<C2S_ChangeShortCutIndex>(NetMessageType.C2S_ChangeShortCutIndex, new C2S_ChangeShortCutIndex
        //    {
        //        itemIndex = oldSlot.index,
        //        shortCutIndex = newSlot.index
        //    });
        //}
    }

    public override void OnClose()
    {
        base.OnClose();
        Clear();
    }
}
