using JKFrame;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class ClientGlobal : SingletonMono<ClientGlobal>
{
    public GameSetting gameSetting { get; private set; }

    private void Start()    
    {
        DontDestroyOnLoad(gameObject);
        
        Application.targetFrameRate = 60;

        NetworkVariableSerializationBinder.Init();

        LoadGameSetting();

        ResSystem.InstantiateGameObject<NetManager>("NetworkManager").Init(true);

        EventSystem.AddTypeEventListener<GameSceneLaunchEvent>(OnGameSceneLaunchEvent);

        LocalizationSystem.GlobalConfig = ResSystem.LoadAsset<LocalizationConfig>("GlobalLocalizationConfig");

        InitUIWindows();

        LoadLoginScene();

        NetMessageManager.Instance.RegisterOnReceiveMessageCallback(NetMessageType.S2C_Disconnect, OnReceiveDisconnect);
    }

    private UI_GamePopupWindow popupWindow;
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            // 只有在GameScene能呼出Escape弹窗
            if (SceneManager.GetActiveScene().name != "GameScene") return;
            if(popupWindow == null || !popupWindow.gameObject.activeSelf)
            {
                UISystem.Show<UI_GamePopupWindow>();
                if (popupWindow == null) popupWindow = UISystem.GetWindow<UI_GamePopupWindow>();
            }
            else
            {
                UISystem.Close<UI_GamePopupWindow>();
            }

        }
    }

    private void OnReceiveDisconnect(ulong clientId, INetworkSerializable serializable)
    {
        C2S_Disconnect C2S_disconnectInfo = (C2S_Disconnect)serializable;
        if(C2S_disconnectInfo.errorType == NetMessageErrorCode.AccountRepeatLogin)
        {
            UISystem.Show<UI_MessagePopUp>().ShowMessageByLocalizationKey(LocalizationKey.accountRepectLogin, Color.red);
            Invoke(nameof(LoadLoginScene), 2f);
        }
        else
        {
            LoadLoginScene();
        }
    }

    private void InitUIWindows()
    {
        UISystem.AddUIWindowData<UI_MainMenuWindow>(new UIWindowData(false, nameof(UI_MainMenuWindow), 0));
        UISystem.AddUIWindowData<UI_MessagePopUp>(new UIWindowData(true, nameof(UI_MessagePopUp), 4));
        UISystem.AddUIWindowData<UI_RegisterWindow>(new UIWindowData(true, nameof(UI_RegisterWindow), 1));
        UISystem.AddUIWindowData<UI_LoginWindow>(new UIWindowData(true, nameof(UI_LoginWindow), 1));
        UISystem.AddUIWindowData<UI_GamePopupWindow>(new UIWindowData(true, nameof(UI_GamePopupWindow), 4));
        UISystem.AddUIWindowData<UI_GameSettingsWindow>(new UIWindowData(true, nameof(UI_GameSettingsWindow), 2));
    }
    private void OnGameSceneLaunchEvent(GameSceneLaunchEvent @event)
    {
        ResSystem.InstantiateGameObject("ClientOnGameScene");
    }

    public void LoadGameSetting()
    {
        gameSetting = SaveSystem.LoadSetting<GameSetting>();
        if (gameSetting == null)
        {
            gameSetting = new GameSetting();
            gameSetting.language = LocalizationSystem.LanguageType;
            gameSetting.musicValue = 1;
            gameSetting.musicEffValue = 1;
        }
        LocalizationSystem.LanguageType = gameSetting.language;
    }

    public void SaveGameSetting()
    {
        SaveSystem.SaveSetting(gameSetting);
    }

    public void LoadLoginScene()
    {
        UISystem.CloseAllWindow();
        Addressables.LoadSceneAsync("LoginScene").WaitForCompletion();
    }

    public void LoadGameScene()
    {
        UISystem.CloseAllWindow();
        SceneSystem.LoadScene("GameScene");
    }
}
