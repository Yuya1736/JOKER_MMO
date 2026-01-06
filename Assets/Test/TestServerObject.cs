using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class TestServerObject : NetworkBehaviour
{
    public float speed = 2;
    public static TestServerObject instance;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        instance = this;
#if UNITY_SERVER || SERVER_EDITOR_TEST
        AOIManager.Instance.InitServerObject(NetworkObject, Vector2Int.zero);
#endif
    }

    void FixedUpdate()
    {
#if UNITY_SERVER || SERVER_EDITOR_TEST
        if (IsServer && IsOwner)
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            Vector2 dir = new Vector2(x, y).normalized;
            HandlerMovement(dir);
        }
#endif
    }

    private Vector2Int oldChunkCoord = Vector2Int.zero;
    private Vector2Int newChunkCoord = Vector2Int.zero;

    //private void HandlerMovement(Vector2 dir)
    //{
    //    oldChunkCoord = AOIManager.Instance.GetChunkCoordByWorldPosition(transform.position);
    //    transform.Translate(new Vector3(dir.x, 0, dir.y) * speed * Time.deltaTime);
    //    newChunkCoord = AOIManager.Instance.GetChunkCoordByWorldPosition(transform.position);

    //    if (oldChunkCoord != newChunkCoord)
    //        AOIManager.Instance.UpdateClientVisualChunk(OwnerClientId, oldChunkCoord, newChunkCoord);
    //}
}
