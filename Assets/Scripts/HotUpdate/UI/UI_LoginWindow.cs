using JKFrame;
using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class UI_LoginWindow : UI_WindowBase
{
    [SerializeField] private InputField inputUserName;
    [SerializeField] private InputField inputPassword;
    [SerializeField] private Button btnLogin ;
    [SerializeField] private Button btnClose;
    [SerializeField] private Button btnRegister;
    [SerializeField] private Toggle togRemember;


    private void Awake()
    {
        btnLogin.onClick.AddListener(OnBtnLoginClick);
        btnClose.onClick.AddListener(OnBtnCloseClick);
        btnRegister.onClick.AddListener(OnBtnRegisterClick);
        togRemember.onValueChanged.AddListener(OnTogRememberChanged);
        inputUserName.onValueChanged.AddListener(OnInputUserNameChanged);
        inputPassword.onValueChanged.AddListener(OnInputPasswordChanged);
        NetMessageManager.Instance.RegisterOnReceiveMessageCallback(NetMessageType.S2C_Login, OnReceiveS2C_Login);
    }

    private void OnReceiveS2C_Login(ulong clientId, INetworkSerializable serializable)
    {
        S2C_Login s2C_Login = (S2C_Login)serializable;
        NetMessageErrorCode errorType = s2C_Login.errorType;
        switch (errorType)
        {
            case NetMessageErrorCode.None:
                UISystem.Show<UI_MessagePopUp>().ShowMessageByLocalizationKey(LocalizationKey.loginSucceed, Color.green);
                ClientGlobal.Instance.LoadGameScene();
                break;
            case NetMessageErrorCode.AccountFormat:
                UISystem.Show<UI_MessagePopUp>().ShowMessageByLocalizationKey(LocalizationKey.userInfoFormatWrong, Color.red);
                break;
            case NetMessageErrorCode.NameOrPassword:
                UISystem.Show<UI_MessagePopUp>().ShowMessageByLocalizationKey(LocalizationKey.nameOrPasswordWrong, Color.red);
                break;
            default:
                break;
        }
    }

    public override void OnShow()
    {
        if (togRemember.isOn)
        {
            inputUserName.text = ClientGlobal.Instance.gameSetting.userName;
            inputPassword.text = ClientGlobal.Instance.gameSetting.password;
        }
        else
        {
            inputUserName.text = "";
            inputPassword.text = "";
        }
    }

    public override void OnClose()
    {
        ClientGlobal.Instance.SaveGameSetting();
    }

    private void OnInputPasswordChanged(string password)
    {
        ClientGlobal.Instance.gameSetting.password = password;
    }

    private void OnInputUserNameChanged(string userName)
    {
        ClientGlobal.Instance.gameSetting.userName = userName;
    }

    private void OnTogRememberChanged(bool isOn)
    {
        ClientGlobal.Instance.gameSetting.UI_LoginWindowTogRemember = isOn;
    }
    private void OnBtnRegisterClick()
    {
        UISystem.Close<UI_LoginWindow>();
        UISystem.Show<UI_RegisterWindow>();
    }
    private void OnBtnCloseClick()
    {
        UISystem.Close<UI_LoginWindow>();
    }

    private void OnBtnLoginClick()
    {
        if (AccountUtility.CheckAccount(inputUserName.text) && AccountUtility.CheckPassword(inputPassword.text))
        {
            NetMessageManager.Instance.SendMessageToServer<C2S_Login>(NetMessageType.C2S_Login, new C2S_Login
            {
                accountInfo = new AccountInfo
                {
                    playerName = inputUserName.text,
                    password = inputPassword.text
                }
            });
        }
        else
        {
            if (!AccountUtility.CheckAccount(inputUserName.text)) UISystem.Show<UI_MessagePopUp>().ShowMessageByLocalizationKey(LocalizationKey.accountFormatWrong, Color.red);
            else UISystem.Show<UI_MessagePopUp>().ShowMessageByLocalizationKey(LocalizationKey.passwordFormatWrong, Color.red);
        }
    }
}
