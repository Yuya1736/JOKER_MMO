using JKFrame;
using System;
using UnityEngine;

public class ServerGlobal : SingletonMono<ServerGlobal>
{
    [SerializeField] private ServerConfig serverConfig;
    [SerializeField] private MapConfig mapConfig;
    public ServerConfig ServerConfig => serverConfig;
    public MapConfig MapConfig => mapConfig;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        Init();
    }

    public void Init()
    {
        Application.targetFrameRate = 30;
          
        NetworkVariableSerializationBinder.Init();

        EventSystem.AddTypeEventListener<GameSceneLaunchEvent>(onGameSceneLaunchEvent);
    }

    private void onGameSceneLaunchEvent(GameSceneLaunchEvent @event)
    {
        ServerResSystem.InsatantialteServerOnGameScene();
    }
}
