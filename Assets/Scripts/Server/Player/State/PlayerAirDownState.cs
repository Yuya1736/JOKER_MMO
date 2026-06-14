using UnityEngine;

public class PlayerAirDownState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();
        player.PlayAnimation(AnimationEvent.AirDown);
        // AirDown movement is driven by prediction/authority motor, not animation root motion.
        player.SetHasGravity(false);
    }

    public override void Update()
    {
        base.Update();
        UpdateTurnDir();
        if (player.PredictionIsGrounded)
        {
            if (player.inputData.dir.x != 0 || player.inputData.dir.y != 0) player.ChangeState(PlayerState.Move);
            else player.ChangeState(PlayerState.Idle);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
