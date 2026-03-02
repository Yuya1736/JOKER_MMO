using System;
using UnityEngine;

public class PlayerJumpState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();
        player.PlayAnimation(AnimationEvent.Jump);
        player.playerView.rootMotionAction += OnRootMotion;
        player.playerView.onJumpStartEndAcion += OnJumpEnd;
        player.SetHasGravity(false);
    }

    private void OnJumpEnd()
    {
        player.ChangeState(PlayerState.AirDown);
    }

    public override void Update()
    {
        base.Update();
        UpdateTurnDir();
    }

    private void OnRootMotion(Vector3 deltaVector, Quaternion deltaQuaternion)
    {
        //player.animator.speed = player.speed;
        deltaVector.x += player.inputData.dir.x * player.airSpeed * Time.deltaTime;
        deltaVector.z += player.inputData.dir.y * player.airSpeed * Time.deltaTime;
        deltaVector.y *= player.jumpHeight;
        player.characterController?.Move(deltaVector);
    }

    public override void Exit()
    {
        base.Exit();

        player.playerView.rootMotionAction -= OnRootMotion;
        player.playerView.onJumpStartEndAcion -= OnJumpEnd;
    }
}
