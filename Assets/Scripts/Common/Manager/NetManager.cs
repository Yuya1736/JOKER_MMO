using JKFrame;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetManager : NetworkManager
{
    [SerializeField] private GameObject Player;

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

        prefabHandlerDic = new Dictionary<GameObject, NetworkPrefabrInstanceHandler>(NetworkConfig.Prefabs.Prefabs.Count);
        foreach (NetworkPrefab networkPrefab in NetworkConfig.Prefabs.Prefabs)
        {
            NetworkPrefabrInstanceHandler handler = new NetworkPrefabrInstanceHandler(networkPrefab.Prefab);
            PrefabHandler.AddHandler(networkPrefab.Prefab, handler);
            prefabHandlerDic.Add(networkPrefab.Prefab, handler);
        }

        if (isClient) InitClient();
        else InitServer();

        netMessageManager.Init();
    }
       
    private void InitClient()
    {
        if(StartClient())
        {
            //SceneManager.PostSynchronizationSceneUnloading = true;
        }
    }

    private void InitServer()
    {
        if(StartServer())
        {
            //SceneManager.SetClientSynchronizationMode(LoadSceneMode.Additive);
        }
    }

    public NetworkObject SpawnObject(ulong clientId, GameObject prefab, Vector3 pos, Quaternion rotation)
    {
        //GameObject player = Instantiate(prefab);
        //NetworkObject networkObject = player.GetComponent<NetworkObject>();
        //player.transform.position = pos;
        //player.transform.rotation = rotation;
        
        //NetworkObject networkObject = ResSystem.InstantiateGameObject<NetworkObject>("Player");
        NetworkObject networkObject = prefabHandlerDic[prefab].Instantiate(clientId, pos, rotation);
        networkObject.SpawnWithOwnership(clientId);
        return networkObject;
    }
    public void DeSpawnObject(NetworkObject networkObject)
    {
        networkObject.Despawn();
    }
}
