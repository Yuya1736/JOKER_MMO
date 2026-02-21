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
    public static UI_SlotBase enteredSlot;
    public IBagWindow bagWindow;
    public int index;
    public Action<int> onMouseRightClickAciton;
    public Action<int> onMouseLeftClickAciton;
    public Action<UI_SlotBase, UI_SlotBase> onDragItemToNewSlotAciton;

    public virtual void Init(IBagWindow bagWindow, ItemDataBase itemData, ItemConfigBase itemConfig, int index, Action<int> onMouseRightClick, Action<int> onMouseLeftClickAciton, Action<UI_SlotBase, UI_SlotBase> onDragItemToNewSlotAciton)
    {
        this.bagWindow = bagWindow;
        this.index = index;
        this.onMouseRightClickAciton = onMouseRightClick;
        this.onMouseLeftClickAciton = onMouseLeftClickAciton;
        this.onDragItemToNewSlotAciton = onDragItemToNewSlotAciton;
        OnPointerExit(null);
    }

    public void Destroy()
    {
        this.GameObjectPushPool();
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        slotImage.sprite = selectFrame;
        enteredSlot = this;
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        slotImage.sprite = normalFrame;
        if (enteredSlot == this) enteredSlot = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            onMouseRightClickAciton?.Invoke(index);
        }
        else if(eventData.button == PointerEventData.InputButton.Left)
        {
            onMouseLeftClickAciton?.Invoke(index);
        }
    }

    private void OnDestroy()
    {
        if (enteredSlot == this) enteredSlot = null;
    }
}

public class UI_SlotBase<D, C> : UI_SlotBase, IBeginDragHandler, IDragHandler, IEndDragHandler where D : ItemDataBase where C : ItemConfigBase
{
    public D itemData;
    public C itemConfig;

    public override void Init(IBagWindow bagWindow, ItemDataBase itemData, ItemConfigBase itemConfig, int index, Action<int> onMouseRightClick, Action<int> onMouseLeftClickAciton, Action<UI_SlotBase, UI_SlotBase> onDragItemToNewSlotAciton)
    {
        base.Init(bagWindow, itemData, itemConfig, index, onMouseRightClick, onMouseLeftClickAciton, onDragItemToNewSlotAciton);
        this.itemData = (D)itemData;
        this.itemConfig = (C)itemConfig;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        iconImage.transform.SetParent(UISystem.DragLayer);
        iconImage.GetComponent<CanvasGroup>().blocksRaycasts = false; // 鼠标进入slot需要高亮显示，不能让iconImage挡住射线
    }

    public void OnDrag(PointerEventData eventData)
    {
        iconImage.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        iconImage.transform.SetParent(this.transform);
        iconImage.transform.SetAsFirstSibling();
        iconImage.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        iconImage.GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (enteredSlot != null && enteredSlot != this) onDragItemToNewSlotAciton?.Invoke(this, enteredSlot);
        enteredSlot = null;
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