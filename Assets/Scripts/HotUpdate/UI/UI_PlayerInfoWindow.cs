using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerInfoWindow : UI_CustomWindowBase
{
    [SerializeField] private Image imgHp;
    private float maxHpWidth = 0;

    public void UpdateHp(float fillAmount)
    {
        if(maxHpWidth == 0)
        {
            maxHpWidth = imgHp.rectTransform.sizeDelta.x;
        }

        imgHp.rectTransform.sizeDelta = new Vector2(maxHpWidth * fillAmount, imgHp.rectTransform.sizeDelta.y);
    }
}
