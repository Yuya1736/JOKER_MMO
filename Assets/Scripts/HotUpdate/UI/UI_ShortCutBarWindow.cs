using JKFrame;
using System.Collections.Generic;
using UnityEngine;

public class UI_ShortCutBarWindow : UI_CustomWindowBase, IBagWindow
{
    public List<Transform> itemRoots = new List<Transform>(GlobalUtility.shortCutNum);
    public List<UI_SlotBase> slotList = new List<UI_SlotBase>(GlobalUtility.shortCutNum);

    private string emptySlotkey => GlobalUtility.emptySlotKey;

    public void Show(BagData bagData)
    {
        Clear(); // 快捷栏比较少 就不用单独Update更新物品了 直接更新整个快捷栏
        for (int i = 0; i < GlobalUtility.shortCutNum; ++i)
        {
            int itemIndex = bagData.itemIndexInShortCut[i];
            if (itemIndex == -1) slotList.Add(CreateEmptySlot(i));
            else
            {
                ItemDataBase itemData = bagData.itemDataList[itemIndex];
                if (itemData != null) slotList.Add(CreateSlot(i, itemData));
                else slotList.Add(CreateEmptySlot(i));
            }
            if (itemIndex == bagData.usedWeponIndex) // 当前使用的武器需要加上UsedIcon
            {
                if (slotList[i] is UI_WeaponSlot)
                {
                    ((UI_WeaponSlot)slotList[i]).SetUseState(true);
                }
                else
                {
                    Debug.Log($"2对应usedWeponIndex: {bagData.usedWeponIndex} 不是WeaponSlot");
                }
            }
        }
    }

    public void UpdateItem(int shortCutIndex)
    {
        slotList[shortCutIndex].Destroy();
        int itemIndex = PlayerManager.bagData.itemIndexInShortCut[shortCutIndex];
        if (itemIndex == -1) slotList[shortCutIndex] = CreateEmptySlot(shortCutIndex);
        else slotList[shortCutIndex] = CreateSlot(shortCutIndex, PlayerManager.bagData.itemDataList[itemIndex]);
        if (PlayerManager.bagData.usedWeponIndex == PlayerManager.bagData.itemIndexInShortCut[shortCutIndex])
        {
            ((UI_WeaponSlot)slotList[shortCutIndex]).SetUseState(true);
        }
    }

    public UI_SlotBase CreateEmptySlot(int index)
    {
        Transform itemRoot = itemRoots[index];
        UI_SlotBase emptySlot = ResSystem.InstantiateGameObject<UI_SlotBase>(emptySlotkey, itemRoot);
        emptySlot.Init(this, null, null, index, null, null, null);
        InitSlotRectTranfom(emptySlot);
        return emptySlot;
    }

    public UI_SlotBase CreateSlot(int index, ItemDataBase itemData)
    {
        Transform itemRoot = itemRoots[index];
        ItemConfigBase itemConfig = ResSystem.LoadAsset<ItemConfigBase>(itemData.id);
        string slotKey = itemConfig.slotKey;
        UI_SlotBase slot = ResSystem.InstantiateGameObject<UI_SlotBase>(slotKey, itemRoot);
        slot.Init(this, itemData, itemConfig, index, OnUseItem, null, OnDragItemToNewSlot);
        InitSlotRectTranfom(slot);
        return slot;
    }

    private void OnDragItemToNewSlot(UI_SlotBase oldSlot, UI_SlotBase newSlot) // oldSlot.bagWindow is UI_ShortCutBar
    {
        if (newSlot.bagWindow is UI_BagWindow) // 快捷栏->背包
        {
            NetMessageManager.Instance.SendMessageToServer<C2S_ChangeShortCutIndex>(NetMessageType.C2S_ChangeShortCutIndex, new C2S_ChangeShortCutIndex
            {
                shortCutIndex = oldSlot.index,
                itemIndex = -1
            });
        }
        else if (newSlot.bagWindow is UI_ShortCutBarWindow) // 快捷栏->快捷栏
        {
            NetMessageManager.Instance.SendMessageToServer<C2S_ExchangeShortCut>(NetMessageType.C2S_ExchangeShortCut, new C2S_ExchangeShortCut
            {
                shortCutIndex1 = oldSlot.index,
                shortCutIndex2 = newSlot.index
            });
        }
    }

    private void InitSlotRectTranfom(UI_SlotBase slot) // 防止Slot从对象池拿出后，位置放置错误
    {
        slot.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        slot.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0);
        slot.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
        slot.GetComponent<RectTransform>().pivot = new Vector2(0, 0);
        slot.transform.SetAsFirstSibling();
    }

    public void OnUseItem(int index)
    {
        PlayerManager.Instance.UseItem(index);
    }

    public override void OnClose()
    {
        base.OnClose();
        Clear();
    }

    public void Clear()
    {
        for (int i = 0; i < slotList.Count; ++i)
        {
            slotList[i].Destroy();
        }
        slotList.Clear();
    }
}
