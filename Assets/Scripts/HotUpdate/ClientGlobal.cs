using JKFrame;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class ClientGlobal : SingletonMono<ClientGlobal>
{
    public GameSetting gameSetting { get; private set; }
    private Stack<UI_WindowBase> blockerInputUIStack = new Stack<UI_WindowBase>(10);
    public Vector2 canvasSize = new Vector2(1920, 1080);

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

        EventSystem.AddTypeEventListener<CheckUIInputBlockerEvent>(OnCheckUIInputBlocker);
    }

    

    private UI_GamePopupWindow popupWindow;

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.B))
        //{
        //    if (UISystem.GetWindow<UI_BagWindow>() == null || !UISystem.GetWindow<UI_BagWindow>().gameObject.activeInHierarchy)
        //    {
        //        //NetMessageManager.Instance.SendMessageToServer<C2S_GetBagData>(NetMessageType.C2S_GetBagData, new C2S_GetBagData
        //        //{
        //        //    version = bagData == null ? -1 : bagData.version
        //        //});
        //        BagData bagData = new BagData();
        //        bagData.itemDataList[0] = new WeaponData() { configKey = ItemConfigKey.weapon0 };
        //        bagData.itemDataList[1] = new WeaponData() { configKey = ItemConfigKey.weapon1 };
        //        bagData.itemDataList[2] = new MaterialData() { configKey = ItemConfigKey.material0, count = 11 };
        //        bagData.itemDataList[3] = new MaterialData() { configKey = ItemConfigKey.material1, count = 22 };
        //        bagData.itemDataList[4] = new MaterialData() { configKey = ItemConfigKey.material2, count = 33 };
        //        bagData.itemDataList[5] = new MaterialData() { configKey = ItemConfigKey.material3, count = 44 };
        //        bagData.itemDataList[6] = new ConsumableData() { configKey = ItemConfigKey.consumable0, count = 1 };
        //        bagData.itemDataList[7] = new ConsumableData() { configKey = ItemConfigKey.consumable1, count = 2 };
        //        bagData.itemDataList[8] = new ConsumableData() { configKey = ItemConfigKey.consumable2, count = 3 };
        //        bagData.itemDataList[9] = new ConsumableData() { configKey = ItemConfigKey.consumable3, count = 4 };
        //        bagData.itemDataList[10] = new ConsumableData() { configKey = ItemConfigKey.consumable4, count = 5 };
        //        UISystem.Show<UI_BagWindow>().Show(bagData);
        //    }
        //    else
        //    {
        //        UISystem.Close<UI_BagWindow>();
        //    }
        //}
        if (SceneManager.GetSceneByName("GameScene").IsValid())
        {
            // 游戏内ESC菜单
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // 只有在GameScene能呼出Escape弹窗
                if (popupWindow == null || !popupWindow.gameObject.activeSelf)
                {
                    UISystem.Show<UI_GamePopupWindow>();
                    if (popupWindow == null) popupWindow = UISystem.GetWindow<UI_GamePopupWindow>();
                }
                else
                {
                    UISystem.Close<UI_GamePopupWindow>();
                }
            }
            // Alt控制显隐鼠标
            if (Input.GetKey(KeyCode.LeftAlt))
            {
                SetCursorLockState(false);
            }
            else if (blockerInputUIStack.Count == 0)
            {
                SetCursorLockState(true);
            }
        }
    }

    public void SetCursorLockState(bool isLocked)
    {
        if (isLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // 恢复摄像机输入
            if (!PlayerManager.Instance) return;
            PlayerManager.Instance.FreeLook.m_XAxis.m_InputAxisName = "Mouse X";
            PlayerManager.Instance.FreeLook.m_YAxis.m_InputAxisName = "Mouse Y";
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // 摄像机不能输入
            if (!PlayerManager.Instance) return;
            PlayerManager.Instance.FreeLook.m_XAxis.m_InputAxisName = "";
            PlayerManager.Instance.FreeLook.m_YAxis.m_InputAxisName = "";
            PlayerManager.Instance.FreeLook.m_XAxis.m_InputAxisValue = 0f;
            PlayerManager.Instance.FreeLook.m_YAxis.m_InputAxisValue = 0f;
        }
    }

    private void OnReceiveDisconnect(ulong clientId, INetworkSerializable serializable)
    {
        S2C_Disconnect S2C_DisconnectInfo = (S2C_Disconnect)serializable;
        if(S2C_DisconnectInfo.errorType == NetMessageErrorCode.AccountRepeatLogin)
        {
            UISystem.Show<UI_MessagePopUp>().ShowMessageByLocalizationKey(LocalizationKey.accountRepectLogin, Color.red);
            Invoke(nameof(LoadLoginScene), 2f);
        }
        else
        {
            LoadLoginScene();
        }
    }

    private void OnCheckUIInputBlocker(CheckUIInputBlockerEvent @event)
    {
        UI_WindowBase window = @event.uI_Window;
        bool isEnter = @event.isEnter;
        if(window is IInputBlockerUI)
        {
            if(isEnter)
                blockerInputUIStack.Push(window);
            else 
                blockerInputUIStack.Pop();
        }
        if (blockerInputUIStack.Count == 0)
        {
            SetCursorLockState(true);
            if (PlayerManager.localPlayer != null) PlayerManager.localPlayer.canControl = true;
        }
        else
        {
            SetCursorLockState(false);
            if (PlayerManager.localPlayer != null) PlayerManager.localPlayer.canControl = false;
        }
    }

    private void InitUIWindows()
    {
        UISystem.AddUIWindowData<UI_MainMenuWindow>(new UIWindowData(false, nameof(UI_MainMenuWindow), 0));
        UISystem.AddUIWindowData<UI_MessagePopUp>(new UIWindowData(true, nameof(UI_MessagePopUp), 4));
        UISystem.AddUIWindowData<UI_RegisterWindow>(new UIWindowData(false, nameof(UI_RegisterWindow), 1));
        UISystem.AddUIWindowData<UI_LoginWindow>(new UIWindowData(false, nameof(UI_LoginWindow), 1));
        UISystem.AddUIWindowData<UI_GamePopupWindow>(new UIWindowData(false, nameof(UI_GamePopupWindow), 1));
        UISystem.AddUIWindowData<UI_GameSettingsWindow>(new UIWindowData(false, nameof(UI_GameSettingsWindow), 2));
        UISystem.AddUIWindowData<UI_ChatWindow>(new UIWindowData(false, nameof(UI_ChatWindow), 1));
        UISystem.AddUIWindowData<UI_ChatWindowItem>(new UIWindowData(false, nameof(UI_ChatWindowItem), 1)); // TODO:这里应该不需要
        UISystem.AddUIWindowData<UI_BagWindow>(new UIWindowData(true, nameof(UI_BagWindow), 2));
        UISystem.AddUIWindowData<UI_ItemInfoPopupWindow>(new UIWindowData(true, nameof(UI_ItemInfoPopupWindow), 2));
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
        SceneManager.LoadScene("GameScene", LoadSceneMode.Additive);
    }
}
