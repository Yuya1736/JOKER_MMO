using JKFrame;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class UI_MessagePopUp : UI_WindowBase
{
    [SerializeField] private Image imgMessageBgLine;
    [SerializeField] private Image imgIcon;
    [SerializeField] private Text textMessage;
    [SerializeField] private Animation anim;


    private void Awake()
    {
        if(anim == null) anim = GetComponent<Animation>();
    }
    public void ShowMessageByLocalizationKey(string key, Color color)
    {
        string message = LocalizationSystem.GetContent<LocalizationStringData>(key, LocalizationSystem.LanguageType).content;
        ShowMessage(message, color);
    }

    [Button]
    public void ShowMessage(string message, Color color)
    {
        imgIcon.color = color;
        imgMessageBgLine.color = color;
        textMessage.text = message;
        textMessage.color = Color.black;
        anim.Play();
    }

    private void OnAnimationEnd()
    {
        anim.Stop();
        UISystem.Close<UI_MessagePopUp>();
    }
}
