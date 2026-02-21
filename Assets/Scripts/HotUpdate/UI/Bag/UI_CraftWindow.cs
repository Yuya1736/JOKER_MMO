using JKFrame;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CraftWindow : UI_CustomWindowBase, IInputBlockerUI, IBagWindow
{
    [SerializeField] private Button btnClose;
    [SerializeField] private Button btnCraft;
    [SerializeField] private Transform itemRoot;
    [SerializeField] private Transform targerItemRoot;
    [SerializeField] private Transform targerChildItemRoot;
    private string emptySlotkey => GlobalUtility.emptySlotKey;
    private List<UI_SlotBase> itemSlotList = new List<UI_SlotBase>(10);
    private const int maxItemSlotCount = 6;
    private UI_SlotBase targerItemSlot;
    private ItemDataBase targetItemData;
    private List<UI_SlotBase> targetChildItemList = new List<UI_SlotBase>(10);

    private void Awake()
    {
        btnClose.onClick.AddListener(OnBtnCloseClick);
        btnCraft.onClick.AddListener(OnBtnCraftClick);
    }

    private void OnBtnCloseClick()
    {
        UISystem.Close<UI_BagWindow>();
        UISystem.Close<UI_CraftWindow>();
    }

    public void ClearItemSlot()
    {
        for (int i = itemSlotList.Count - 1; i >= 0; i--)
        {
            itemSlotList[i].Destroy();
        }
        itemSlotList.Clear();
    }

    public void ClearTargetSlot()
    {
        if (targerItemSlot != null) targerItemSlot.Destroy();
        targerItemSlot = null;
        for (int i = targetChildItemList.Count - 1; i >= 0; i--)
        {
            targetChildItemList[i].Destroy();
        }
        targetChildItemList.Clear();
    }

    public void Show(CrafterConfig crafterConfig)
    {
        ClearItemSlot();

        List<CrafterItemConfig> itemConfigs = crafterConfig.itemConfigs;
        for (int i = 0; i < maxItemSlotCount; i++)
        {
            if (i >= itemConfigs.Count || itemConfigs[i].count <= 0) itemSlotList.Add(CreateEmptySlot(i, itemRoot));
            else
            {
                CrafterItemConfig craftItemConfig = itemConfigs[i];
                ItemConfigBase itemConfigBase = craftItemConfig.itemConfig;
                int count = craftItemConfig.count;
                ItemDataBase itemDataBase  = itemConfigBase.GetDefaultItemData();
                if (itemDataBase is StackableItemDataBase) ((StackableItemDataBase)itemDataBase).count = count;
                UI_SlotBase slot = CreateSlot(i, itemDataBase, itemRoot);
                itemSlotList.Add(slot);
            }
        }
    }

    private void ShowSelectItem(ItemConfigBase itemConfig, int count)
    {
        ClearTargetSlot();

        ItemDataBase itemDataBase = itemConfig.GetDefaultItemData();
        if (itemDataBase is StackableItemDataBase) ((StackableItemDataBase)itemDataBase).count = count;
        targerItemSlot = CreateSlot(0, itemDataBase, targerItemRoot);
        targetItemData = itemDataBase;
        foreach (var craftItemDic in itemConfig.craftItemDic)
        {
            string craftItemId = craftItemDic.Key;
            int craftItemCount = craftItemDic.Value;
            ItemConfigBase craftItemConfig = ResSystem.LoadAsset<ItemConfigBase>(craftItemId);
            ItemDataBase craftItemData = craftItemConfig.GetDefaultItemData();
            if (craftItemData is StackableItemDataBase) ((StackableItemDataBase)craftItemData).count = craftItemCount;
            UI_SlotBase slot = CreateSlot(count, craftItemData, targerChildItemRoot);
            targetChildItemList.Add(slot);
            count++;
        }
    }

    public void OnSelectItem(int index)
    {
        if (PlayerManager.currentCrafterConfig != null)
        {
            CrafterConfig crafterConfig = ResSystem.LoadAsset<CrafterConfig>(PlayerManager.currentCrafterConfig);
            ItemConfigBase targetItemConfig = crafterConfig.itemConfigs[index].itemConfig;
            ShowSelectItem(targetItemConfig, crafterConfig.itemConfigs[index].count);
        }
    }

    private void OnBtnCraftClick()
    {
        if (targetItemData == null) return;
        PlayerManager.Instance.CraftItem(targetItemData);
    }

    public UI_SlotBase CreateEmptySlot(int index, Transform itemRoot)
    {
        //UI_SlotBase emptySlot = Instantiate(ResSystem.LoadAsset<GameObject>(emptySlotkey), itemRoot).GetComponent<UI_SlotBase>();
        UI_SlotBase emptySlot = ResSystem.InstantiateGameObject<UI_SlotBase>(emptySlotkey, itemRoot);
        emptySlot.Init(this, null, null, index, null, null, null);
        emptySlot.transform.SetSiblingIndex(index);
        return emptySlot;
    }

    public UI_SlotBase CreateSlot(int index, ItemDataBase itemData, Transform itemRoot)
    {
        ItemConfigBase itemConfig = ResSystem.LoadAsset<ItemConfigBase>(itemData.id);
        string slotKey = itemConfig.slotKey;
        UI_SlotBase slot = ResSystem.InstantiateGameObject<UI_SlotBase>(slotKey, itemRoot);
        slot.Init(this, itemData, itemConfig, index, null, OnSelectItem, OnDragItemToNewSlot);
        slot.transform.SetSiblingIndex(index);
        return slot;
    }

    private void OnDragItemToNewSlot(UI_SlotBase oldSlot, UI_SlotBase newSlot) // oldSlot.bagWindow is UI_BagWindow
    {
        
    }

    public override void OnClose()
    {
        base.OnClose();
        ClearItemSlot();
        ClearTargetSlot();
    }
}
