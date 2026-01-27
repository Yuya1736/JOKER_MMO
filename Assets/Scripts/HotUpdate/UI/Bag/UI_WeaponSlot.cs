using System;
using UnityEngine;

public class UI_WeaponSlot : UI_SlotBase<WeaponData, WeaponConfig>
{
    [SerializeField] private GameObject selectIconPrefab; // TODO:

    public override void Init(ItemDataBase itemData, ItemConfigBase itemConfig, int index, Action<int> onMouseRightClick)
    {
        base.Init(itemData, itemConfig, index, onMouseRightClick);
        iconImage.sprite = itemConfig.icon;
    }
}