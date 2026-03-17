using JKFrame;
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
    public CharacterControllerBase player;
    public Vector2Int coord;
}

public struct UpdateClientAOIEvent
{
    public CharacterControllerBase player;
    public Vector2Int oldCoord;
    public Vector2Int newCoord;
}

