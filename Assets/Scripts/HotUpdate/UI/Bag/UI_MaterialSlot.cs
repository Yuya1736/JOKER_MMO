using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_MaterialSlot : UI_SlotBase<MaterialData, MaterialConfig>
{
    [SerializeField] private Text textCount;
    public int itemCount;

    public override void Init(ItemDataBase itemData, ItemConfigBase itemConfig, int index, Action<int> onMouseRightClick = null)
    {
        base.Init(itemData, itemConfig, index, null);
        itemCount = ((MaterialData)itemData).count;
        iconImage.sprite = itemConfig.icon;
        UpdateCount();
    }

    public void UpdateCount()
    {
        textCount.text = itemCount.ToString();
    }
}