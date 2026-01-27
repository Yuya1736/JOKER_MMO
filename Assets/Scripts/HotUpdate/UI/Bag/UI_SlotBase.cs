using JKFrame;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SlotBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Image slotImage;
    [SerializeField] protected Image iconImage;
    [SerializeField] private Sprite normalFrame;
    [SerializeField] private Sprite selectFrame;
    public int index;
    public Action<int> onMouseRightClick;

    public virtual void Init(ItemDataBase itemData, ItemConfigBase itemConfig, int index, Action<int> onMouseRightClick)
    {
        this.index = index;
        this.onMouseRightClick = onMouseRightClick;
        OnPointerExit(null);
    }

    public void Destroy()
    {
        this.GameObjectPushPool();
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        slotImage.sprite = selectFrame;
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        slotImage.sprite = normalFrame;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            onMouseRightClick?.Invoke(index);
        }
    }
}

public class UI_SlotBase<D, C> : UI_SlotBase where D : ItemDataBase where C : ItemConfigBase
{
    public D itemData;
    public C itemConfig;

    public override void Init(ItemDataBase itemData, ItemConfigBase itemConfig, int index, Action<int> onMouseRightClick)
    {
        base.Init(itemData, itemConfig, index, onMouseRightClick);
        this.itemData = (D)itemData;
        this.itemConfig = (C)itemConfig;
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        UISystem.Show<UI_ItemInfoPopupWindow>().Init(transform.position, itemConfig);
    }
     
    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        if (UISystem.GetWindow<UI_ItemInfoPopupWindow>())UISystem.Close<UI_ItemInfoPopupWindow>();
    }
}