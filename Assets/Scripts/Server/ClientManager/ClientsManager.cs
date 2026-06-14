using JKFrame;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public partial class ClientsManager : SingletonMono<ClientsManager>
{
    public GameObject player;

    public Dictionary<ClientState, HashSet<Client>> clientStateDic;// 连接时立马加入
    public Dictionary<ulong, Client> clientIdDic;  // 连接时立马加入
    public Dictionary<string, ulong> accountDic; // Login后才会加入
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    public void Init()
    {
        clientIdDic = new Dictionary<ulong, Client>(100);
        accountDic = new Dictionary<string, ulong>(100);
        clientStateDic = new Dictionary<ClientState, HashSet<Client>>(100)
        {
            {ClientState.Connected, new HashSet<Client>(100)},
            {ClientState.Logined, new HashSet<Client>(100)},
            {ClientState.Gaming, new HashSet<Client>(100)}
        };

        NetManager.Instance.OnClientConnectedCallback += OnClientConnected;
        NetManager.Instance.OnClientDisconnectCallback += OnClientDisconnected;

        InitLoginSystem();
        InitChatSystem();
        InitItemSystem();
        InitTaskSystem();
        InitPredictionSystem();
    }

    private void OnClientConnected(ulong clientId)
    {
        Client client = ResSystem.GetOrNew<Client>(nameof(Client));
        client.clientId = clientId;
        clientIdDic.Add(clientId, client);
        ChangeClientState(clientId, ClientState.Connected);
    }

    private void OnClientDisconnected(ulong clientId)
    {
        if (clientIdDic.TryGetValue(clientId, out Client client))
        {
            if (client.playerData != null) accountDic.Remove(client.playerData.name);
            if (client.playerController != null) NetManager.Instance.DeSpawnObject(client.playerController.NetworkObject);
            clientIdDic.Remove(clientId);
            clientStateDic[client.state].Remove(client);
            client.Destroy();
        }
    }

    public void ChangeClientState(ulong clientId, ClientState newState)
    {
        Client client = clientIdDic[clientId];

        if (clientStateDic[client.state].Contains(client)) clientStateDic[client.state].Remove(client);
        client.state = newState;
        clientStateDic[client.state].Add(client);
    }

    #region TestButton
    [Button]
    public void Spawn1()
    {
        NetManager.Instance.SpawnObject(1, player, ServerGlobal.Instance.ServerConfig.defaultPlayerBirthPos, default);
    }
    [Button]
    public void Spawn2()
    {
        NetManager.Instance.SpawnObject(2, player, ServerGlobal.Instance.ServerConfig.defaultPlayerBirthPos, default);
    }
    [Button]
    public void PrintClients()
    {
        print("-----------------");
        print("------可见性------");
        print($"clientIdDic[1].playerController.NetworkObject.IsNetworkVisibleTo(2):{clientIdDic[1].playerController.NetworkObject.IsNetworkVisibleTo(2)}");
        print($"clientIdDic[2].playerController.NetworkObject.IsNetworkVisibleTo(1):{clientIdDic[2].playerController.NetworkObject.IsNetworkVisibleTo(1)}");
        print("-----------------");
        print("accountDic");
        foreach (var item in accountDic)
        {
            print($"{item.Key} + {item.Value}");
        }
        print("-----------------");
        print("clientStateDic");
        foreach (var item in clientStateDic[ClientState.Gaming])
        {
            print($"{item.clientId}");
        }
        print("-----------------");
        print("clientIdDic");
        foreach (var item in clientIdDic)
        {
            print($"{item.Key} + {item.Value.clientId}");
        }
        print("-----------------");
    }
    #endregion
}
