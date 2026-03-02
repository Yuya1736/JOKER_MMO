using UnityEngine;

public class PlayerIdleState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();
        player.PlayAnimation(AnimationEvent.Idle);
    }

    public override void Update()
    {
        base.Update();
        if (player.inputData.atk)
        {
            player.ChangeState(PlayerState.Atk);
            player.inputData.atk = false;
            return;
        }
        if (player.inputData.jump)
        {
            player.ChangeState(PlayerState.Jump);
            player.inputData.jump = false;
            return;
        }
        if (player.inputData.dir != Vector2.zero)
        {
            player.ChangeState(PlayerState.Move);
            return;
        }
    }
}