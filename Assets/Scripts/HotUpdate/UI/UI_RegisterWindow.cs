using JKFrame;
using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class UI_RegisterWindow : UI_WindowBase
{
    [SerializeField] private InputField inputUserName;
    [SerializeField] private InputField inputPassword;
    [SerializeField] private InputField inputRePassword;
    [SerializeField] private Button btnLogin;
    [SerializeField] private Button btnRegister;
    [SerializeField] private Button btnClose;
    public void Awake()
    {
        btnLogin.onClick.AddListener(OnBtnLoginClick);
        btnRegister.onClick.AddListener(OnBtnRegisterClick);
        btnClose.onClick.AddListener(OnBtnCloseClick);
    }
    public override void OnShow()
    {
        inputUserName.text = "";
        inputPassword.text = "";
        inputRePassword.text = "";
        NetMessageManager.Instance.RegisterOnReceiveMessageCallback(NetMessageType.S2C_Register, OnReceiveS2C_Register);
    }

    public override void OnClose()
    {
        NetMessageManager.Instance.UnRegisterOnReceiveMessageCallback(NetMessageType.S2C_Register, OnReceiveS2C_Register);
    }
    private void OnReceiveS2C_Register(ulong CLientId, INetworkSerializable serializable)
    {
        S2C_Register s2C_Register = (S2C_Register)serializable;
        NetMessageErrorCode errorType = s2C_Register.errorType;
        switch (errorType)
        {
            case NetMessageErrorCode.None:
                UISystem.Show<UI_MessagePopUp>().ShowMessageByLocalizationKey(LocalizationKey.registerSucceed, Color.green);
                break;
            case NetMessageErrorCode.AccountFormat:
                UISystem.Show<UI_MessagePopUp>().ShowMessageByLocalizationKey(LocalizationKey.userInfoFormatWrong, Color.red);
                break;
            case NetMessageErrorCode.NameDuplicaiton:
                UISystem.Show<UI_MessagePopUp>().ShowMessageByLocalizationKey(LocalizationKey.nameDuplicaiton, Color.red);
                break;
            default:
                break;
        }
    }

    private void OnBtnCloseClick()
    {
        UISystem.Close<UI_RegisterWindow>();
    }

    private void OnBtnRegisterClick()
    {
        if(AccountUtility.CheckAccount(inputUserName.text) && AccountUtility.CheckPassword(inputPassword.text))
        {
            if(inputPassword.text == inputRePassword.text)
            {
                NetMessageManager.Instance.SendMessageToServer<C2S_Register>(NetMessageType.C2S_Register, new C2S_Register
                {
                    accountInfo = new AccountInfo()
                    { 
                        playerName = inputUserName.text,
                        password = inputPassword.text
                    }
                });
            }
            else
            {
                UISystem.Show<UI_MessagePopUp>().ShowMessageByLocalizationKey(LocalizationKey.rePasswordWrong, Color.red);
            }
        }
        else
        {
            if(!AccountUtility.CheckAccount(inputUserName.text)) UISystem.Show<UI_MessagePopUp>().ShowMessageByLocalizationKey(LocalizationKey.accountFormatWrong, Color.red);
            else UISystem.Show<UI_MessagePopUp>().ShowMessageByLocalizationKey(LocalizationKey.passwordFormatWrong, Color.red);
        }
    }

    private void OnBtnLoginClick()
    {
        UISystem.Close<UI_RegisterWindow>();
        UISystem.Show<UI_LoginWindow>();
    }
}
