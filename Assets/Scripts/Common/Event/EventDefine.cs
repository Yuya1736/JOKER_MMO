using JKFrame;
using Unity.Netcode;
using UnityEngine;

public struct BulletSpawnEvent
{
    public BulletController mainBulletController;
}

public struct MonsterSpawnEvent
{
    public MonsterController mainMonsterController;
}

public struct PlayerSpawnEvent
{
    public PlayerController mainPlayerController;
}

public struct InitClientAOIEvent
{
    public ulong clientId;
    public Vector2Int coord;
}

public struct UpdateClientAOIEvent
{
    public ulong clientId;
    public Vector2Int oldCoord;
    public Vector2Int newCoord;
}

public struct InitServerObjectAOIEvent
{
    public NetworkObject networkObject;
    public Vector2Int coord;
}

public struct UpdateServerObjectAOIEvent
{
    public NetworkObject networkObject;
    public Vector2Int oldCoord;
    public Vector2Int newCoord;
}



