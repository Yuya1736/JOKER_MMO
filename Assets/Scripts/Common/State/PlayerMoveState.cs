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

    private float nowEulerY;
    private float currentVelocity;
    public override void Update()
    {
        base.Update();
        if (player.inputData.dir == Vector2.zero) player.ChangeState(PlayerState.Idle);
        else
        {
            nowEulerY = player.PlayerView.transform.eulerAngles.y;
            float tanRad = Mathf.Atan2(player.inputData.dir.x, player.inputData.dir.y);
            float tanDeg = Mathf.Rad2Deg * tanRad;
            nowEulerY = Mathf.SmoothDampAngle(nowEulerY, tanDeg, ref currentVelocity, 0.1f);
            player.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, nowEulerY, player.transform.eulerAngles.z);
        }
    }

    private void OnRootMotion(Vector3 deltaVector, Quaternion deltaQuaternion)
    {
        player.Animator.speed = player.speed;
        //deltaVector.y -= player.verticalVelocity * Time.deltaTime;

        oldChunkCoord = AOIUtility.GetChunkCoordByWorldPosition(player.transform.position);
        player.CharacterController.Move(deltaVector);
        newChunkCoord = AOIUtility.GetChunkCoordByWorldPosition(player.transform.position);
        if (oldChunkCoord != newChunkCoord) player.UpdateClientVisualChunk(oldChunkCoord, newChunkCoord);
    }

    public override void Exit()
    {
        base.Exit();

        player.PlayerView.RemoveAction(OnRootMotion);
    }
}
#endif