using JKFrame;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemInfoPopupWindow : UI_WindowBase
{
    [SerializeField] private Text textName;
    [SerializeField] private Text textType;
    [SerializeField] private Text textDescription;
    [SerializeField] private Image imgIcon;
    private Vector2 CanvasSize => ClientGlobal.Instance.canvasSize;
    private RectTransform rectTransform => (RectTransform)transform;

    public void Init(Vector3 slotWorldPos, ItemConfigBase itemConfig, float topOffset = 10f)
    {
        Vector2 windowSize = ((RectTransform)transform).sizeDelta;
        Vector2 widthRange = new Vector2(CanvasSize.x / -2 + windowSize.x / 2, CanvasSize.x / 2 - windowSize.x / 2);
        Vector2 heightRange = new Vector2(CanvasSize.y / -2 + windowSize.y / 2, CanvasSize.y / 2 - windowSize.y / 2);
        transform.position = slotWorldPos;
        Vector2 windowPos = ((RectTransform)transform).anchoredPosition;
        windowPos.x = Mathf.Clamp(windowPos.x, widthRange.x, widthRange.y);
        windowPos.y = Mathf.Clamp(windowPos.y, heightRange.x, heightRange.y);
        rectTransform.anchoredPosition = windowPos;

        imgIcon.sprite = itemConfig.icon;
        textName.text = itemConfig.GetItemName(LocalizationSystem.LanguageType);
        textType.text = itemConfig.GetItemType(LocalizationSystem.LanguageType);
        textDescription.text = itemConfig.GetItemDescription(LocalizationSystem.LanguageType);
    }

}
