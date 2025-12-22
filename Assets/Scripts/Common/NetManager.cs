using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public class NetManager : NetworkManager
{
    private static NetManager instance;
    public static  NetManager Instance => instance;

    public UnityTransport unityTransport;

    public void Init(bool isClient)
    {
        instance = this;
        unityTransport = GetComponent<UnityTransport>();

        if (isClient) InitClient();
        else InitServer();
    }

    private void InitClient()
    {
        StartClient();
        print("CLient start");
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
