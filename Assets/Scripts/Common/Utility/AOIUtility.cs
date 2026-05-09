using JKFrame;
using Unity.Netcode;
using UnityEngine;

public static class AOIUtility
{
    private static float chunkSize = 50;

    public static Vector2Int GetChunkCoordByWorldPosition(Vector3 worldPosition)
    {
        return new Vector2Int((int)(worldPosition.x / chunkSize), (int)(worldPosition.z / chunkSize));
    }

    public static void InitClientVisualChunk(ulong clientId, Vector2Int chunkCoord)
    {
        EventSystem.TypeEventTrigger<InitClientAOIEvent>(new InitClientAOIEvent()
        {
            clientId = clientId,
            coord = chunkCoord
        });
    }

    public static void UpdateClientVisualChunk(ulong clientId, Vector2Int oldChunkCoord, Vector2Int newChunkCoord)
    {
        EventSystem.TypeEventTrigger<UpdateClientAOIEvent>(new UpdateClientAOIEvent()
        {
            clientId = clientId,
            oldCoord = oldChunkCoord,
            newCoord = newChunkCoord
        });
    }
    public static void InitServerObjectVisualChunk(NetworkObject networkObject, Vector2Int chunkCoord)
    {
        EventSystem.TypeEventTrigger<InitServerObjectAOIEvent>(new InitServerObjectAOIEvent()
        {
            networkObject = networkObject,
            coord = chunkCoord
        });
    }

    public static void UpdateServerObjectVisualChunk(NetworkObject networkObject, Vector2Int oldChunkCoord, Vector2Int newChunkCoord)
    {
        EventSystem.TypeEventTrigger<UpdateServerObjectAOIEvent>(new UpdateServerObjectAOIEvent()
        {
            networkObject = networkObject,
            oldCoord = oldChunkCoord,
            newCoord = newChunkCoord
        });
    }
}
