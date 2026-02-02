using System;
using UnityEngine;

public class UI_WeaponSlot : UI_SlotBase<WeaponData, WeaponConfig>
{
    [SerializeField] private GameObject selectIcon; // TODO:

    public override void Init(ItemDataBase itemData, ItemConfigBase itemConfig, int index, Action<int> onMouseRightClick)
    {
        base.Init(itemData, itemConfig, index, onMouseRightClick);
        iconImage.sprite = itemConfig.icon;
    }

    public void SetUseState(bool isUsed)
    {
        if (isUsed)
        {
            selectIcon.SetActive(true);
        }
        else
        {
            selectIcon.SetActive(false);
        }
    }
}