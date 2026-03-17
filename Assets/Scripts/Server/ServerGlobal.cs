using JKFrame;
using System;
using System.Collections.Generic;
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
        InitNetworkSideControllerDic();
        Init();
    }

    public void Init()
    {
        Application.targetFrameRate = 30;
          
        NetworkVariableSerializationBinder.Init();

        EventSystem.AddTypeEventListener<GameSceneLaunchEvent>(onGameSceneLaunchEvent);
    }

    private void InitNetworkSideControllerDic()
    {
        NetworkEntityBase.sideControllerDic = new Dictionary<Type, Type>()
        {
            {typeof(PlayerController), typeof(PlayerServerController)},
            {typeof(MonsterController), typeof(MonsterServerController)},
            {typeof(BulletController), typeof(BulletServerController)}
        };
    }

    private void onGameSceneLaunchEvent(GameSceneLaunchEvent @event)
    {
        ServerResSystem.InsatantialteServerOnGameScene();
    }
}
