#if UNITY_SERVER || UNITY_EDITOR
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
        if (player.inputData.dir != Vector2.zero) player.ChangeState(PlayerState.Move);
    }
}
#endif