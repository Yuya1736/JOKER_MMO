using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkPrefabrInstanceHandler : INetworkPrefabInstanceHandler
{
    public GameObject prefab;

    private Queue<NetworkObject> networkObjectPool = new Queue<NetworkObject>(100);

    public NetworkPrefabrInstanceHandler(GameObject prefab)
    {
        this.prefab = prefab;
    }

    public void Destroy(NetworkObject networkObject)
    {
        networkObject.gameObject.SetActive(false);
        networkObjectPool.Enqueue(networkObject);
        //networkObject.GameObjectPushPool();
    }

    public NetworkObject Instantiate(ulong ownerClientId, Vector3 position, Quaternion rotation)
    {
        //NetworkObject networkObject = PoolSystem.GetGameObject<NetworkObject>(prefab.name);
        NetworkObject networkObject = null;
        if (networkObjectPool.Count > 0)
        {
            networkObject = networkObjectPool.Dequeue();
            networkObject.transform.position = position;
            networkObject.transform.rotation = rotation;
            networkObject.gameObject.SetActive(true);
        }
        else networkObject = GameObject.Instantiate(prefab, position, rotation).GetComponent<NetworkObject>();
        networkObject.gameObject.name = prefab.name; // 防止生成时会自己加上(clone），导致对象池的名称不匹配 

        return networkObject;
    }
}
