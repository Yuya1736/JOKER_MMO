using UnityEngine;

public class MonsterPatrolState : MonsterStateBase
{
    private float timer;
    private Vector3 targerPos;

    public override void Enter()
    {
        base.Enter();
        monster.PlayAnimation(AnimationEvent.Move);
        timer = monster.mainController.config.maxPatrolTime;
        targerPos = monster.GetRandomPatrolPosition();
        monster.agent.SetDestination(targerPos);
        monster.agent.isStopped = false;
    }

    public override void Update()
    {
        base.Update();
        timer -= Time.deltaTime;
        // 追踪玩家
        if (monster.chasePlayer != null)
        {
            monster.ChangeState(MonsterState.chase);
        }
        // 待机
        bool arrivedAtTarget = !monster.agent.pathPending && (monster.transform.position - targerPos).sqrMagnitude < 1f;
        if (timer <= 0 || arrivedAtTarget)
        {
            monster.agent.isStopped = true;
            monster.ChangeState(MonsterState.Idle);
        }
    }
}