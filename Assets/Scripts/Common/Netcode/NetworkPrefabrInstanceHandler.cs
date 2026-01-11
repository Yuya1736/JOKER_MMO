using JKFrame;
using Unity.Netcode;
using UnityEngine;
using System.IO;

public class NetworkPrefabrInstanceHandler : INetworkPrefabInstanceHandler
{
    public GameObject prefab;

    public NetworkPrefabrInstanceHandler(GameObject prefab)
    {
        this.prefab = prefab;
    }

    public void Destroy(NetworkObject networkObject)
    {
        networkObject.GameObjectPushPool();
    }

    public NetworkObject Instantiate(ulong ownerClientId, Vector3 position, Quaternion rotation)
    {
        NetworkObject networkObject = PoolSystem.GetGameObject<NetworkObject>(prefab.name);

        if (networkObject == null)
        {
            networkObject = GameObject.Instantiate(prefab).GetComponent<NetworkObject>();
            networkObject.gameObject.name = prefab.name; // 防止生成时会自己加上(clone），导致对象池的名称不匹配 
        }
        networkObject.transform.position = position;
        networkObject.transform.rotation = rotation;

        return networkObject;
    }
}
