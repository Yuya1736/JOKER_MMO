using JKFrame;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ClientsManager : SingletonMono<ClientsManager>
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
        NetMessageManager.Instance.RegisterOnReceiveMessageCallback(NetMessageType.C2S_Register, OnReceiveRegisterMessage);
        NetMessageManager.Instance.RegisterOnReceiveMessageCallback(NetMessageType.C2S_Login, OnReceiveLoginMessage);
        NetMessageManager.Instance.RegisterOnReceiveMessageCallback(NetMessageType.C2S_EnterGame, OnReceiveEnterGameMessage);
        NetMessageManager.Instance.RegisterOnReceiveMessageCallback(NetMessageType.C2S_Disconnect, OnReceiveDisconnectMessage);
        NetMessageManager.Instance.RegisterOnReceiveMessageCallback(NetMessageType.C2S_Chat, OnReceiveChatMessage);
        NetMessageManager.Instance.RegisterOnReceiveMessageCallback(NetMessageType.C2S_GetBagData, OnReceiveGetBagDataMessage);
        NetMessageManager.Instance.RegisterOnReceiveMessageCallback(NetMessageType.C2S_UseItem, OnReceiveUseItemMessage);
    }

    private void OnReceiveUseItemMessage(ulong clientId, INetworkSerializable serializable)
    {
#if UNITY_EDITOR || UNITY_SERVER
        C2S_UseItem message = (C2S_UseItem)serializable;
        Client client = clientIdDic[clientId];
        PlayerData playerData = client.playerData;
        S2C_UpdateBagData result = new S2C_UpdateBagData()
        {
            index = message.index,
            itemType = ItemType.Empty,
            itemData = null,
            version = -1
        };
        if(playerData != null && playerData.bagData != null)
        {
            ItemDataBase itemData = playerData.bagData.UseItem(message.index);
            result.itemData = itemData;
            result.version = ++ playerData.bagData.version;
            if (itemData != null) result.itemType = itemData.GetItemType();
        }
        NetMessageManager.Instance.SendMessageToClient<S2C_UpdateBagData>(clientId, NetMessageType.S2C_UpdateBagData, result);
#endif
    }

    private void OnReceiveGetBagDataMessage(ulong clientId, INetworkSerializable serializable)
    {
        C2S_GetBagData c2S_GetBagInfo = (C2S_GetBagData)serializable;
        Client client = clientIdDic[clientId];
        S2C_GetBagData s2C_GetBagDataInfo = new S2C_GetBagData { haveBag = false };
        if(client.playerData.bagData.version != c2S_GetBagInfo.version)
        {
            s2C_GetBagDataInfo.haveBag = true;
            s2C_GetBagDataInfo.bagData = client.playerData.bagData;

            s2C_GetBagDataInfo.bagData.itemDataList[0] = new WeaponData() { configKey = ItemConfigKey.weapon0 };
            s2C_GetBagDataInfo.bagData.itemDataList[1] = new WeaponData() { configKey = ItemConfigKey.weapon1 };
            s2C_GetBagDataInfo.bagData.itemDataList[2] = new MaterialData() { configKey = ItemConfigKey.material0, count = 11 };
            s2C_GetBagDataInfo.bagData.itemDataList[3] = new MaterialData() { configKey = ItemConfigKey.material1, count = 22 };
            s2C_GetBagDataInfo.bagData.itemDataList[4] = new MaterialData() { configKey = ItemConfigKey.material2, count = 33 };
            s2C_GetBagDataInfo.bagData.itemDataList[5] = new MaterialData() { configKey = ItemConfigKey.material3, count = 44 };
            s2C_GetBagDataInfo.bagData.itemDataList[6] = new ConsumableData() { configKey = ItemConfigKey.consumable0, count = 1 };
            s2C_GetBagDataInfo.bagData.itemDataList[7] = new ConsumableData() { configKey = ItemConfigKey.consumable1, count = 2 };
            s2C_GetBagDataInfo.bagData.itemDataList[8] = new ConsumableData() { configKey = ItemConfigKey.consumable2, count = 3 };
            s2C_GetBagDataInfo.bagData.itemDataList[9] = new ConsumableData() { configKey = ItemConfigKey.consumable3, count = 4 };
            s2C_GetBagDataInfo.bagData.itemDataList[10] = new ConsumableData() { configKey = ItemConfigKey.consumable4, count = 5 };
        }
        NetMessageManager.Instance.SendMessageToClient<S2C_GetBagData>(clientId, NetMessageType.S2C_GetBagData, s2C_GetBagDataInfo);
    }

    private void OnReceiveChatMessage(ulong clientId, INetworkSerializable serializable)
    {
        C2S_Chat c2S_ChatInfo = (C2S_Chat)serializable;
        Client client = clientIdDic[clientId];
        string playerName = client.playerData.name;
        // 分发给所有Gaming客户端
        foreach (var item in clientStateDic[ClientState.Gaming])
        {
            if (item.clientId == clientId) continue; // 如果对应客户端的ID和发送客户端的ID相同，则不需要发送，发送消息的客户端自身就会显示
            NetMessageManager.Instance.SendMessageToClient<S2C_Chat>(item.clientId, NetMessageType.S2C_Chat, new S2C_Chat
            {
                errorType = NetMessageErrorCode.None,
                name = playerName,
                info = c2S_ChatInfo.info
            });
        }
    }

    private void OnReceiveDisconnectMessage(ulong clientId, INetworkSerializable serializable)
    {
        // 收到客户端断联消息后，清除当前客户端的登录数据
        Client client = clientIdDic[clientId];
        if (client.playerData != null) accountDic.Remove(client.playerData.name);
        if (client.playerController != null) NetManager.Instance.DeSpawnObject(client.playerController.NetworkObject);
        client.playerData = null;
        client.playerController = null;
        ChangeClientState(clientId, ClientState.Connected);
        // 收到客户端发来的断联请求后，需要先Despawn掉Player(NetworkObject)再发消息让客户端退出，否则客户端会在加载到别的场景时销毁Player（只有服务端可以销毁，否则会报错Destroy Invalid）
        NetMessageManager.Instance.SendMessageToClient<S2C_Disconnect>(clientId, NetMessageType.S2C_Disconnect, new S2C_Disconnect { errorType = NetMessageErrorCode.None });
    }

    private void OnReceiveEnterGameMessage(ulong clientId, INetworkSerializable serializable)
    {
        Client client = clientIdDic[clientId];
        if (client.playerController != null) return;
        if (client.state == ClientState.Gaming) return;
        PlayerData playerData = client.playerData;
        CharacterData characterData = playerData.characterData;
        ChangeClientState(clientId, ClientState.Gaming);
        NetworkObject networkObject = NetManager.Instance.SpawnObject(clientId, player, characterData.position, Quaternion.Euler(0, characterData.rotate_Y, 0));
        client.playerController = networkObject.gameObject.GetComponent<PlayerController>();
    }

    private void OnReceiveLoginMessage(ulong clientId, INetworkSerializable serializable)
    {
        C2S_Login c2S_Login = (C2S_Login)serializable;
        AccountInfo accountInfo = c2S_Login.accountInfo;
        S2C_Login s2C_Login = new S2C_Login { errorType = NetMessageErrorCode.None};

        if (!AccountUtility.CheckAccount(accountInfo.playerName) || !AccountUtility.CheckPassword(accountInfo.password))
        {
            s2C_Login.errorType = NetMessageErrorCode.AccountFormat;
        }
        else if(DataBaseManager.Instance.GetPlayerData(accountInfo.playerName) == null || DataBaseManager.Instance.GetPlayerData(accountInfo.playerName).password != accountInfo.password)
        {
            s2C_Login.errorType = NetMessageErrorCode.NameOrPassword;
        }
        else
        {
            // 如果账户已经被登录，则挤下线
            if (accountDic.TryGetValue(accountInfo.playerName, out ulong oldClientId))
            {
                Client oldClient = clientIdDic[oldClientId];
                // 清除旧客户端登录后加载的所有数据
                if (oldClient.playerData != null) accountDic.Remove(oldClient.playerData.name);
                if (oldClient.playerController != null) NetManager.Instance.DeSpawnObject(oldClient.playerController.NetworkObject);
                oldClient.playerData = null;
                oldClient.playerController = null;
                ChangeClientState(oldClientId, ClientState.Connected);
                // 向客户端发送断联消息
                NetMessageManager.Instance.SendMessageToClient<S2C_Disconnect>(oldClientId, NetMessageType.S2C_Disconnect, new S2C_Disconnect
                {
                    errorType = NetMessageErrorCode.AccountRepeatLogin
                });
            }
            // 绑定Client的PlayerData
            PlayerData playerData = DataBaseManager.Instance.GetPlayerData(accountInfo.playerName);
            clientIdDic[clientId].playerData = playerData;
            playerData.bagData.version = 0;
            ChangeClientState(clientId, ClientState.Logined);
            accountDic.Add(playerData.name, clientId);
        }
        NetMessageManager.Instance.SendMessageToClient<S2C_Login>(clientId, NetMessageType.S2C_Login, s2C_Login);
    }

    private void OnReceiveRegisterMessage(ulong clientId, INetworkSerializable serializable)
    {
        C2S_Register c2S_Register = (C2S_Register)serializable;
        AccountInfo accountInfo = c2S_Register.accountInfo;
        S2C_Register s2C_Register =  new S2C_Register { errorType = NetMessageErrorCode.None};

        // 服务端对客户端不信任，再次验证账号密码格式
        if (!AccountUtility.CheckAccount(accountInfo.playerName) || !AccountUtility.CheckPassword(accountInfo.password))
        {
            s2C_Register.errorType = NetMessageErrorCode.AccountFormat;
        }
        else if(DataBaseManager.Instance.GetPlayerData(accountInfo.playerName) != null)
        {
            s2C_Register.errorType = NetMessageErrorCode.NameDuplicaiton;
        }
        else
        {
            PlayerData playerData = new PlayerData() { name = accountInfo.playerName, password = accountInfo.password};
            DataBaseManager.Instance.AddPlayerData(playerData);
        }
        NetMessageManager.Instance.SendMessageToClient<S2C_Register>(clientId, NetMessageType.S2C_Register, s2C_Register);
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
        if(clientIdDic.TryGetValue(clientId, out Client client))
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