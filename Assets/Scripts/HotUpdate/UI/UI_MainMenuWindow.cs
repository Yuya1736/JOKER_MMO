using JKFrame;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainMenuWindow : UI_WindowBase
{
    [SerializeField] private Button btnLogin;
    [SerializeField] private Button btnRegister;
    [SerializeField] private Button btnSetting;
    [SerializeField] private Button btnExit;
    private void Awake()
    {
        btnLogin.onClick.AddListener(onClickBtnLogin);
        btnRegister.onClick.AddListener(onClickBtnRegister);
        btnSetting.onClick.AddListener(onClickBtnSetting);
        btnExit.onClick.AddListener(onClickBtnExit);
    }

    private void onClickBtnSetting()
    {
        UISystem.Show<UI_GameSettingsWindow>();
    }

    private void onClickBtnExit()
    {
        Application.Quit();
    }

    private void onClickBtnRegister()
    {
        UISystem.Show<UI_RegisterWindow>();
    }

    private void onClickBtnLogin()
    {
        UISystem.Show<UI_LoginWindow>();
    }
}
