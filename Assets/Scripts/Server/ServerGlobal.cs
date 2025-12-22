using JKFrame;
using Unity.Netcode;
using UnityEngine;

public class ServerGlobal : SingletonMono<ServerGlobal>
{
    [SerializeField] ServerConfig serverConfig;
    public ServerConfig ServerConfig => serverConfig;
    protected override void Awake()
    {
        base.Awake();
        Init();
        DontDestroyOnLoad(gameObject);
    }

    public void Init()
    {
        Application.targetFrameRate = 30;

        NetworkVariableSerializationBinder.Init();

        ResSystem.InstantiateGameObject<NetManager>("NetworkManager").Init(false);
        var prefab = NetManager.Instance.gameObject;
        Debug.Log(
            $"Prefab instanceID: {prefab.GetInstanceID()}, " +
            $"Hash: {NetworkManager.Singleton.NetworkConfig.GetHashCode()}"
        );
    }
}
