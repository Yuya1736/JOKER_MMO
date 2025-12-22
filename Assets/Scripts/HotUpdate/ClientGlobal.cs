using JKFrame;
using Unity.Netcode;
using UnityEngine;

public class ClientGlobal : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        Init();

        NetworkVariableSerializationBinder.Init();

        ResSystem.InstantiateGameObject<NetManager>("NetworkManager").Init(true);
        var prefab = NetManager.Instance.gameObject;
        Debug.Log(
            $"Prefab instanceID: {prefab.GetInstanceID()}, " +
            $"Hash: {NetworkManager.Singleton.NetworkConfig.GetHashCode()}"
        );
    }

    private void Init()
    {
        Application.targetFrameRate = 60;
    }
}
