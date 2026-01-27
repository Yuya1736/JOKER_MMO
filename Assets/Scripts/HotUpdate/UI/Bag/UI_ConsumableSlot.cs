using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_ConsumableSlot : UI_SlotBase<ConsumableData, ConsumableConfig>
{
    [SerializeField] private Text textCount; 
    public int itemCount;

    public override void Init(ItemDataBase itemData, ItemConfigBase itemConfig, int index, Action<int> onMouseRightClick)
    {
        base.Init(itemData, itemConfig, index, onMouseRightClick);
        itemCount = ((ConsumableData)itemData).count;
        iconImage.sprite = itemConfig.icon;
        UpdateCount();
    }

    public void UpdateCount()
    {
        textCount.text = itemCount.ToString();
    }
}