using JKFrame;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class AOIManager : SingletonMono<AOIManager>
{
    // 每个区块的大小
    [SerializeField] private float chunkSize = 50;
    // 可视区块范围 (1代表3*3区块)
    [SerializeField] private int visualChunkRange = 1;
    // 默认初始区块，初始化使用
    private Vector2Int defaultChunkCoord = new Vector2Int(-999999, -999999);

    private Dictionary<Vector2Int, HashSet<ulong>> chunkClientDic = new Dictionary<Vector2Int, HashSet<ulong>>();
    private Dictionary<Vector2Int, HashSet<NetworkObject>> chunkServerObjectDic = new Dictionary<Vector2Int, HashSet<NetworkObject>>();

    #region Client

    protected override void Awake()
    {
        base.Awake();

        // 交给AOIUtility
        EventSystem.AddTypeEventListener<InitClientAOIEvent>(OnInitClient);
        EventSystem.AddTypeEventListener<UpdateClientAOIEvent>(OnUpdateClientVisualChunk);
    }
    // --Start-- 交给AOIUtility
    private void OnInitClient(InitClientAOIEvent arg)
    {
        InitClient(arg.player.OwnerClientId, arg.coord);
    }

    private void OnUpdateClientVisualChunk(UpdateClientAOIEvent arg)
    {
        UpdateClientVisualChunk(arg.player.OwnerClientId, arg.oldCoord, arg.newCoord);
    }
    // --End-- 交给AOIUtility
    public void InitClient(ulong clientId, Vector2Int ChunkCoord)
    {
        UpdateClientVisualChunk(clientId, defaultChunkCoord, ChunkCoord);
    }

    public void UpdateClientVisualChunk(ulong clientId, Vector2Int oldChunkCoord, Vector2Int newChunkCoord)
    {
        if (oldChunkCoord == newChunkCoord) return;

        RemoveClient(clientId, oldChunkCoord);

        // 传送属性的跨区域移动导致的区块变化
        if (Vector2Int.Distance(oldChunkCoord, newChunkCoord) > 1.5f)
        {
            for (int x = -visualChunkRange; x <= visualChunkRange; ++x)
                for (int y = -visualChunkRange; y <= visualChunkRange; ++y)
                {
                    Vector2Int _oldChunkCoord = new Vector2Int(oldChunkCoord.x + x, oldChunkCoord.y + y);
                    Vector2Int _newChunkCoord = new Vector2Int(newChunkCoord.x + x, newChunkCoord.y + y);
                    ShowAndHideChunkForClient(clientId, _oldChunkCoord, _newChunkCoord);
                }
        }
        // 普通移动导致的区块变化（只存在上下左右四个方向，左上是左和上的叠加）
        else
        {
            // 上
            if (newChunkCoord.y > oldChunkCoord.y)
            {
                for (int x = -visualChunkRange; x <= visualChunkRange; ++x)
                {
                    Vector2Int _newChunkCoord = new Vector2Int(newChunkCoord.x + x, newChunkCoord.y + visualChunkRange);
                    Vector2Int _oldChunkCoord = new Vector2Int(oldChunkCoord.x + x, oldChunkCoord.y - visualChunkRange);
                    ShowAndHideChunkForClient(clientId, _oldChunkCoord, _newChunkCoord);
                }
            }
            // 下
            if (newChunkCoord.y < oldChunkCoord.y)
            {
                for (int x = -visualChunkRange; x <= visualChunkRange; ++x)
                {
                    Vector2Int _newChunkCoord = new Vector2Int(newChunkCoord.x + x, newChunkCoord.y - visualChunkRange);
                    Vector2Int _oldChunkCoord = new Vector2Int(oldChunkCoord.x + x, oldChunkCoord.y + visualChunkRange);
                    ShowAndHideChunkForClient(clientId, _oldChunkCoord, _newChunkCoord);
                }
            }
            // 左
            if (newChunkCoord.x < oldChunkCoord.x)
            {
                for (int y = -visualChunkRange; y <= visualChunkRange; ++y)
                {
                    Vector2Int _newChunkCoord = new Vector2Int(newChunkCoord.x - visualChunkRange, newChunkCoord.y + y);
                    Vector2Int _oldChunkCoord = new Vector2Int(oldChunkCoord.x + visualChunkRange, oldChunkCoord.y + y);
                    ShowAndHideChunkForClient(clientId, _oldChunkCoord, _newChunkCoord);
                }
            }
            // 右
            if (newChunkCoord.x > oldChunkCoord.x)
            {
                for (int y = -visualChunkRange; y <= visualChunkRange; ++y)
                {
                    Vector2Int _newChunkCoord = new Vector2Int(newChunkCoord.x + visualChunkRange, newChunkCoord.y + y);
                    Vector2Int _oldChunkCoord = new Vector2Int(oldChunkCoord.x - visualChunkRange, oldChunkCoord.y + y);
                    ShowAndHideChunkForClient(clientId, _oldChunkCoord, _newChunkCoord);
                }
            }
        }

        AddClient(clientId, newChunkCoord);
    }

    // 集成了区块客户端和服务端对指定客户端的显示和隐藏
    public void ShowAndHideChunkForClient(ulong clientId, Vector2Int oldChunkCoord, Vector2Int newChunkCoord)
    {
        ShowChunkClientsForClient(clientId, newChunkCoord);
        HideChunkClientsForClient(clientId, oldChunkCoord);
        ShowChunkServerObjectsForClient(clientId, newChunkCoord);
        HideChunkServerObjectsForClient(clientId, oldChunkCoord);
    }
    // 显示区块中的所有客户端
    public void ShowChunkClientsForClient(ulong clientId, Vector2Int chunkCoord)
    {
        if (chunkClientDic.TryGetValue(chunkCoord, out HashSet<ulong> clientsId))
        {
            foreach (ulong otherClientId in clientsId)
            {
                ClientShowEach(clientId, otherClientId);
            }
        }
    }
    // 隐藏区块中的所有客户端
    public void HideChunkClientsForClient(ulong clientId, Vector2Int chunkCoord)
    {
        if (chunkClientDic.TryGetValue(chunkCoord, out HashSet<ulong> clientsId))
        {
            foreach (ulong otherClientId in clientsId)
            {
                ClientHideEach(clientId, otherClientId);
            }
        }
    }
    // 显示区块中的所有服务端给客户端
    public void ShowChunkServerObjectsForClient(ulong clientId, Vector2Int chunkCoord)
    {
        if (chunkServerObjectDic.TryGetValue(chunkCoord, out HashSet<NetworkObject> serverObjects))
        {
            foreach (NetworkObject serverObject in serverObjects)
            {
                if (!serverObject.IsNetworkVisibleTo(clientId)) serverObject.NetworkShow(clientId);
            }
        }
    }
    // 隐藏区块中的所有服务端给客户端
    public void HideChunkServerObjectsForClient(ulong clientId, Vector2Int chunkCoord)
    {
        if (chunkServerObjectDic.TryGetValue(chunkCoord, out HashSet<NetworkObject> serverObjects))
        {
            foreach (NetworkObject serverObject in serverObjects)
            {
                if (serverObject.IsNetworkVisibleTo(clientId)) serverObject.NetworkHide(clientId);
            }
        }
    }

    // 移除区块中的指定客户端
    public void RemoveClient(ulong clientId, Vector2Int chunkCoord)
    {
        if (chunkClientDic.TryGetValue(chunkCoord, out HashSet<ulong> clientsId))
        {
            // 如果该区块已不存在客户端，则释放区块的HashSet节省内存
            if (clientsId.Remove(clientId) && clientsId.Count == 0)
            {
                clientsId.ObjectPushPool();
                chunkClientDic.Remove(chunkCoord);
            }
        }
    }
    // 添加客户端到指定区块
    public void AddClient(ulong clientId, Vector2Int chunkCoord)
    {
        if (!chunkClientDic.TryGetValue(chunkCoord, out HashSet<ulong> clientsId))
        {
            clientsId = ResSystem.GetOrNew<HashSet<ulong>>();
            chunkClientDic.Add(chunkCoord, clientsId);
        }
        clientsId.Add(clientId);
    }

    public void ClientShowEach(ulong clientA, ulong clientB)
    {
        if (clientA == clientB) return;
        if (NetworkManager.Singleton.SpawnManager.OwnershipToObjectsTable.TryGetValue(clientA, out Dictionary<ulong, NetworkObject> dicA) &&
            NetworkManager.Singleton.SpawnManager.OwnershipToObjectsTable.TryGetValue(clientB, out Dictionary<ulong, NetworkObject> dicB))
        {
            foreach (NetworkObject item in dicA.Values)
            {
                if (!item.IsNetworkVisibleTo(clientB)) item.NetworkShow(clientB);
            }
            foreach (NetworkObject item in dicB.Values)
            {
                if (!item.IsNetworkVisibleTo(clientA)) item.NetworkShow(clientA);
            }
        }

    }

    public void ClientHideEach(ulong clientA, ulong clientB)
    {
        if (clientA == clientB) return;
        if (NetworkManager.Singleton.SpawnManager.OwnershipToObjectsTable.TryGetValue(clientA, out Dictionary<ulong, NetworkObject> dicA) &&
            NetworkManager.Singleton.SpawnManager.OwnershipToObjectsTable.TryGetValue(clientB, out Dictionary<ulong, NetworkObject> dicB))
        {
            foreach (NetworkObject item in dicA.Values)
            {
                if (item.IsNetworkVisibleTo(clientB)) item.NetworkHide(clientB);
            }
            foreach (NetworkObject item in dicB.Values)
            {
                if (item.IsNetworkVisibleTo(clientA)) item.NetworkHide(clientA);
            }
        }
    }
    #endregion

    #region ServerObject

    public void InitServerObject(NetworkObject serverObject, Vector2Int ChunkCoord)
    {
        UpdateServerObjectVisualChunk(serverObject, defaultChunkCoord, ChunkCoord);
    }

    public void UpdateServerObjectVisualChunk(NetworkObject serverObject, Vector2Int oldChunkCoord, Vector2Int newChunkCoord)
    {
        if (oldChunkCoord == newChunkCoord) return;

        RemoveServerObject(serverObject, oldChunkCoord);

        // 传送属性的跨区域移动导致的区块变化
        if (Vector2Int.Distance(oldChunkCoord, newChunkCoord) > 1.5f)
        {
            for (int x = -visualChunkRange; x <= visualChunkRange; ++x)
                for (int y = -visualChunkRange; y <= visualChunkRange; ++y)
                {
                    Vector2Int _oldChunkCoord = new Vector2Int(oldChunkCoord.x + x, oldChunkCoord.y + y);
                    Vector2Int _newChunkCoord = new Vector2Int(newChunkCoord.x + x, newChunkCoord.y + y);
                    ShowAndHideChunkForServer(serverObject, _oldChunkCoord, _newChunkCoord);
                }
        }
        // 普通移动导致的区块变化（只存在上下左右四个方向，左上是左和上的叠加）
        else
        {
            // 上
            if (newChunkCoord.y > oldChunkCoord.y)
            {
                for (int x = -visualChunkRange; x <= visualChunkRange; ++x)
                {
                    Vector2Int _newChunkCoord = new Vector2Int(newChunkCoord.x + x, newChunkCoord.y + visualChunkRange);
                    Vector2Int _oldChunkCoord = new Vector2Int(oldChunkCoord.x + x, oldChunkCoord.y - visualChunkRange);
                    ShowAndHideChunkForServer(serverObject, _oldChunkCoord, _newChunkCoord);
                }
            }
            // 下
            if (newChunkCoord.y < oldChunkCoord.y)
            {
                for (int x = -visualChunkRange; x <= visualChunkRange; ++x)
                {
                    Vector2Int _newChunkCoord = new Vector2Int(newChunkCoord.x + x, newChunkCoord.y - visualChunkRange);
                    Vector2Int _oldChunkCoord = new Vector2Int(oldChunkCoord.x + x, oldChunkCoord.y + visualChunkRange);
                    ShowAndHideChunkForServer(serverObject, _oldChunkCoord, _newChunkCoord);
                }
            }
            // 左
            if (newChunkCoord.x < oldChunkCoord.x)
            {
                for (int y = -visualChunkRange; y <= visualChunkRange; ++y)
                {
                    Vector2Int _newChunkCoord = new Vector2Int(newChunkCoord.x + visualChunkRange, newChunkCoord.y + y);
                    Vector2Int _oldChunkCoord = new Vector2Int(oldChunkCoord.x - visualChunkRange, oldChunkCoord.y + y);
                    ShowAndHideChunkForServer(serverObject, _oldChunkCoord, _newChunkCoord);
                }
            }
            // 右
            if (newChunkCoord.x > oldChunkCoord.x)
            {
                for (int y = -visualChunkRange; y <= visualChunkRange; ++y)
                {
                    Vector2Int _newChunkCoord = new Vector2Int(newChunkCoord.x - visualChunkRange, newChunkCoord.y + y);
                    Vector2Int _oldChunkCoord = new Vector2Int(oldChunkCoord.x + visualChunkRange, oldChunkCoord.y + y);
                    ShowAndHideChunkForServer(serverObject, _oldChunkCoord, _newChunkCoord);
                }
            }
        }

        AddServerObject(serverObject, newChunkCoord);
    }

    // 集成了区块客户端对服务端的显示和隐藏
    public void ShowAndHideChunkForServer(NetworkObject serverObject, Vector2Int oldChunkCoord, Vector2Int newChunkCoord)
    {
        ShowChunkClientsForServerObject(serverObject, newChunkCoord);
        HideChunkClientsForServerObject(serverObject, oldChunkCoord);
    }
    // 显示区块中的所有客户端给服务端
    public void ShowChunkClientsForServerObject(NetworkObject serverObject, Vector2Int chunkCoord)
    {
        if (chunkClientDic.TryGetValue(chunkCoord, out HashSet<ulong> clientsId))
        {
            foreach (ulong clientId in clientsId)
            {
                if (!serverObject.IsNetworkVisibleTo(clientId)) serverObject.NetworkShow(clientId);
            }
        }
    }
    // 隐藏区块中的所有客户端给服务端
    public void HideChunkClientsForServerObject(NetworkObject serverObject, Vector2Int chunkCoord)
    {
        if (chunkClientDic.TryGetValue(chunkCoord, out HashSet<ulong> clientsId))
        {
            foreach (ulong clientId in clientsId)
            {
                if (serverObject.IsNetworkVisibleTo(clientId)) serverObject.NetworkHide(clientId);
            }
        }
    }

    // 移除区块中的指定服务端
    public void RemoveServerObject(NetworkObject serverObject, Vector2Int chunkCoord)
    {
        if (chunkServerObjectDic.TryGetValue(chunkCoord, out HashSet<NetworkObject> serverObjects))
        {
            // 如果该区块已不存在客户端，则释放区块的HashSet节省内存
            if (serverObjects.Remove(serverObject) && serverObjects.Count == 0)
            {
                serverObjects.ObjectPushPool();
                chunkServerObjectDic.Remove(chunkCoord);
            }
        }
    }
    // 添加服务端到指定区块
    public void AddServerObject(NetworkObject serverObject, Vector2Int chunkCoord)
    {
        if (!chunkServerObjectDic.TryGetValue(chunkCoord, out HashSet<NetworkObject> serverObjects))
        {
            serverObjects = ResSystem.GetOrNew<HashSet<NetworkObject>>();
            chunkServerObjectDic.Add(chunkCoord, serverObjects);
        }
        serverObjects.Add(serverObject);
    }
    #endregion

    public Vector2Int GetChunkCoordByWorldPosition(Vector3 worldPosition)
    {
        return new Vector2Int((int)(worldPosition.x / chunkSize), (int)(worldPosition.z / chunkSize));
    }

}
