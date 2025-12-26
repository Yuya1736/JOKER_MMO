using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public class NetManager : NetworkManager
{
    private static NetManager instance;
    public static  NetManager Instance => instance;

    public UnityTransport unityTransport;
    public NetMessageManager netMessageManager;

    public void Init(bool isClient)
    {
        instance = this;
        unityTransport = this.GetComponent<UnityTransport>();
        netMessageManager = this.GetComponent<NetMessageManager>();

        if (isClient) InitClient();
        else InitServer();

        netMessageManager.Init();
    }

    private void InitClient()
    {
        StartClient();
    }

    private void InitServer()
    {
        StartServer();
    }

    public NetworkObject SpawnObject(ulong clientId, GameObject prefab, Vector3 pos)
    {
        GameObject g = Instantiate(prefab);
        // 直接获取组件 ？判断是否存在可能更好
        NetworkObject networkObject = g.GetComponent<NetworkObject>();
        g.transform.position = pos;
        networkObject.SpawnWithOwnership(clientId);
        return networkObject;
    }

    
}
