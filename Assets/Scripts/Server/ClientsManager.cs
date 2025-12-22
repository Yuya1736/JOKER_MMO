using JKFrame;
using UnityEngine;

public class ClientsManager : SingletonMono<ClientsManager>
{
    public GameObject player;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    public void Init()
    {
        // 连接成功后传回客户端Id
        NetManager.Instance.OnClientConnectedCallback += OnClientConnectSucceed;
    }

    private void OnClientConnectSucceed(ulong clientId)
    {
        NetManager.Instance.SpawnObject(clientId, player, ServerGlobal.Instance.ServerConfig.defaultPlayerBirthPos);
        print(clientId + "连入");
    }
}
