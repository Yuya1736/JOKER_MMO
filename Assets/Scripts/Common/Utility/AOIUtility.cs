using JKFrame;
using UnityEngine;

public static class AOIUtility
{
    private static float chunkSize = 50;

    public static Vector2Int GetChunkCoordByWorldPosition(Vector3 worldPosition)
    {
        return new Vector2Int((int)(worldPosition.x / chunkSize), (int)(worldPosition.z / chunkSize));
    }

    public static void InitClient(PlayerController playerController, Vector2Int chunkCoord)
    {
        EventSystem.TypeEventTrigger<InitClientAOIEvent>(new InitClientAOIEvent()
        {
            player = playerController,
            coord = chunkCoord
        });
    }

    public static void UpdateClientVisualChunk(PlayerController playerController, Vector2Int oldChunkCoord, Vector2Int newChunkCoord)
    {
        EventSystem.TypeEventTrigger<UpdateClientAOIEvent>(new UpdateClientAOIEvent()
        {
            player = playerController,
            oldCoord = oldChunkCoord,
            newCoord = newChunkCoord
        });
    }
}
