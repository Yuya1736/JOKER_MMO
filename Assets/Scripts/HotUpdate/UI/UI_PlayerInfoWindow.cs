using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerInfoWindow : UI_CustomWindowBase
{
    [SerializeField] private Image imgHp;

    public void UpdateHp(float fillAmount)
    {
        imgHp.fillAmount = fillAmount;
    }
}
