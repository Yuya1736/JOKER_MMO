using UnityEngine;

public class PlayerAirDownState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();
        player.PlayAnimation(AnimationEvent.AirDown);
        player.SetHasGravity(true);
        player.playerView.rootMotionAction += OnRootMotion;
    }

    private void OnRootMotion(Vector3 deltaVector, Quaternion deltaQuaternion)
    {
        deltaVector.x += player.inputData.dir.x * player.airSpeed * Time.deltaTime;
        deltaVector.z += player.inputData.dir.y * player.airSpeed * Time.deltaTime;
        deltaVector.y *= player.jumpHeight;
        player.characterController?.Move(deltaVector);
    }

    public override void Update()
    {
        base.Update();
        UpdateTurnDir();
        if (player.isGrounded)
        {
            if (player.inputData.dir.x != 0 || player.inputData.dir.y != 0) player.ChangeState(PlayerState.Move);
            else player.ChangeState(PlayerState.Idle);
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.playerView.rootMotionAction -= OnRootMotion;
    }
}
