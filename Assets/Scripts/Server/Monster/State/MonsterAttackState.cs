using JKFrame;
using UnityEngine;

public class MonsterAttackState : MonsterStateBase
{
    private float exitTime = 2f;
    private Coroutine timerCoroutine;

    public override void Enter()
    {
        base.Enter();
        monster.PlayAnimation(AnimationEvent.Attack);
        timerCoroutine = TimerUtils.ExecuteAfterDelay(exitTime, () => { monster.ChangeState(MonsterState.Idle) ;});
        //monster.mainController.view.MonsterAttack();
    }

    public override void Exit()
    {
        base.Exit();
        if (timerCoroutine != null)
        {
            TimerUtils.CancelTimer(timerCoroutine);
            timerCoroutine = null;
        }
    }
}