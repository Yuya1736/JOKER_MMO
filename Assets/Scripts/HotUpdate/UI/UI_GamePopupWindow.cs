using JKFrame;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_GamePopupWindow : UI_WindowBase
{
    [SerializeField] private Button btnContinue;
    [SerializeField] private Button btnSetting;
    [SerializeField] private Button btnBackMainMenu;
    [SerializeField] private Button btnExit;

    private void Start()
    {
        btnContinue.onClick.AddListener(OnBtnContinueClick);
        btnSetting.onClick.AddListener(OnBtnSettingClick);
        btnBackMainMenu.onClick.AddListener(OnBtnBackMainMenuClick);
        btnExit.onClick.AddListener(OnBtnExitClick);
    }

    private void OnBtnExitClick()
    {
        Application.Quit();
    }

    private void OnBtnBackMainMenuClick()
    {
        NetMessageManager.Instance.SendMessageToServer<C2S_Disconnect>(NetMessageType.C2S_Disconnect, new C2S_Disconnect
        {
            errorType = NetMessageErrorCode.None
        });
    }

    private void OnBtnSettingClick()
    {
        UISystem.Show<UI_GameSettingsWindow>();
    }

    private void OnBtnContinueClick()
    {
        UISystem.Close<UI_GamePopupWindow>();
    }
}
