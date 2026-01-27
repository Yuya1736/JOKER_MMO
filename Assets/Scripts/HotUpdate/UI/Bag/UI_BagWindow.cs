using JKFrame;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BagWindow : UI_CustomWindowBase, IInputBlockerUI
{
    [SerializeField] private Button btnClose;
    [SerializeField] private Transform itemRoot;
    [SerializeField] private string emptySlotkey;
    private List<UI_SlotBase> slotList = new List<UI_SlotBase>(100);

    private void Awake()
    {
        btnClose.onClick.AddListener(OnBtnCloseClick);
    }

    private void OnBtnCloseClick()
    {
        UISystem.Close<UI_BagWindow>();
    }

    public void Clear()
    {
        foreach (UI_SlotBase slot in slotList)
        {
            slot.Destroy();
        }
        slotList.Clear();
    }

    public void Show(BagData bagData)
    {
        List<ItemDataBase> itemDataList = bagData.itemDataList;
        for (int i = 0; i < itemDataList.Count; ++i) slotList.Add(null);
        int currentIndex = 0;
        foreach (ItemDataBase itemData in itemDataList)
        {
            if (itemData != null)
            {
                slotList[currentIndex] = CreateSlot(currentIndex, itemData);
            }
            else
            {
                slotList[currentIndex] = CreateEmptySlot(currentIndex);
            }
            currentIndex ++;
        }
    }

    public void OnUseItem(int index)
    {
        PlayerManager.Instance.UseItem(index);
    }

    public void UpdataItem(int index, ItemDataBase itemData)
    {
        slotList[index].Destroy();
        if (itemData != null)
        {
            CreateSlot(index, itemData);
        }
        else
        {
            CreateEmptySlot(index);
        }

    }

    public UI_SlotBase CreateEmptySlot(int index)
    {
        //UI_SlotBase emptySlot = Instantiate(ResSystem.LoadAsset<GameObject>(emptySlotkey), itemRoot).GetComponent<UI_SlotBase>();
        UI_SlotBase emptySlot = ResSystem.InstantiateGameObject(emptySlotkey, itemRoot).GetComponent<UI_SlotBase>();
        emptySlot.Init(null, null, index, null);
        emptySlot.transform.SetSiblingIndex(index);
        slotList[index] = emptySlot;
        return emptySlot;
    }

    public UI_SlotBase CreateSlot(int index, ItemDataBase itemData)
    {
        ItemConfigBase itemConfig = ResSystem.LoadAsset<ItemConfigBase>(itemData.configKey);
        string slotKey = itemConfig.slotKey;
        UI_SlotBase slot = ResSystem.InstantiateGameObject(slotKey, itemRoot).GetComponent<UI_SlotBase>();
        slot.Init(itemData, itemConfig, index, OnUseItem);
        slot.transform.SetSiblingIndex(index);
        slotList[index] = slot;
        return slot;
    }

    public override void OnClose()
    {
        base.OnClose();
        Clear();
    }
}
