using UnityEngine;

public struct LocalPlayerEvent
{
    public PlayerController localPlayer;
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