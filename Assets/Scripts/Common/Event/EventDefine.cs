using JKFrame;
using UnityEngine;

public struct PlayerSpawnEvent
{
    public PlayerController mainPlayerController;
}

public struct InitClientAOIEvent
{
    public PlayerController player;
    public Vector2Int coord;
}

public struct UpdateClientAOIEvent
{
    public PlayerController player;
    public Vector2Int oldCoord;
    public Vector2Int newCoord;
}

