using JKFrame;
using UnityEngine;
using UnityEngine.UI;

public class UI_ChatWindowItem : UI_WindowBase
{
    public Text textContent;

    public void Init(string name, string info, bool isOwner = false)
    {
        if (isOwner)
        {
            textContent.alignment = TextAnchor.MiddleRight;
            textContent.text = $"{info} : <color=#7BFF00>{name}</color>";
        }
        else
        {
            textContent.alignment = TextAnchor.MiddleLeft;
            textContent.text = $"<color=yellow>{name}</color> : {info}"; 
        }
    }

}
