#if UNITY_SERVER || UNITY_EDITOR
using UnityEngine;

public class PlayerMoveState : PlayerStateBase
{
    private Vector2Int oldChunkCoord = Vector2Int.zero;
    private Vector2Int newChunkCoord = Vector2Int.zero;

    public override void Enter()
    {
        base.Enter();

        player.PlayAnimation(AnimationEvent.Move);
        player.PlayerView.AddAction(OnRootMotion);
    }

    public override void Update()
    {
        base.Update();
        if (player.inputData.dir == Vector2.zero) player.ChangeState(PlayerState.Idle);
    }

    private void OnRootMotion(Vector3 deltaVector, Quaternion deltaQuaternion)
    {
        player.Animator.speed = player.speed;
        //deltaVector.y -= player.verticalVelocity * Time.deltaTime;

        oldChunkCoord = AOIUtility.GetChunkCoordByWorldPosition(player.transform.position);
        player.CharacterController.Move(deltaVector * Time.deltaTime);
        newChunkCoord = AOIUtility.GetChunkCoordByWorldPosition(player.transform.position);
        if (oldChunkCoord != newChunkCoord) player.UpdateClientVisualChunk(oldChunkCoord, newChunkCoord);
    }
}
#endif