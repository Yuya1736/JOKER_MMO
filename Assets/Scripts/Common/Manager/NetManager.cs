using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public class NetManager : NetworkManager
{
    private static NetManager instance;
    public static  NetManager Instance => instance;

    public UnityTransport unityTransport;
    public NetMessageManager netMessageManager;

    public Dictionary<GameObject, NetworkPrefabrInstanceHandler> prefabHandlerDic;

    public void Init(bool isClient)
    {
        instance = this;
        unityTransport = this.GetComponent<UnityTransport>();
        netMessageManager = this.GetComponent<NetMessageManager>();

        if (isClient) InitClient();
        else InitServer();

        netMessageManager.Init();

        prefabHandlerDic = new Dictionary<GameObject, NetworkPrefabrInstanceHandler>(NetworkConfig.Prefabs.Prefabs.Count);
        foreach (NetworkPrefab networkPrefab in NetworkConfig.Prefabs.Prefabs)
        {
            NetworkPrefabrInstanceHandler handler = new NetworkPrefabrInstanceHandler(networkPrefab.Prefab);
            PrefabHandler.AddHandler(networkPrefab.Prefab, handler);
            prefabHandlerDic.Add(networkPrefab.Prefab, handler);
        }
    }

    private void InitClient()
    {
        StartClient();
    }

    private void InitServer()
    {
        StartServer();
    }
    public NetworkObject SpawnObject(ulong clientId, GameObject prefab, Vector3 pos, Quaternion rotation)
    {
        NetworkObject networkObject = prefabHandlerDic[prefab].Instantiate(clientId, pos, rotation);
        networkObject.SpawnWithOwnership(clientId);
        return networkObject;
    }

    public void DeSpawnObject(NetworkObject networkObject)
    {
        networkObject.Despawn();
    }
}
