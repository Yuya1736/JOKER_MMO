using System;
using UnityEngine;

public class PlayerJumpState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();
        player.PlayAnimation(AnimationEvent.Jump);
        // Jump movement is driven by prediction/authority motor, not animation root motion.
        player.SetHasGravity(false);
    }

    public override void Update()
    {
        base.Update();
        UpdateTurnDir();
        if (player.PredictionVerticalVelocity <= 0f)
        {
            player.ChangeState(PlayerState.AirDown);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
