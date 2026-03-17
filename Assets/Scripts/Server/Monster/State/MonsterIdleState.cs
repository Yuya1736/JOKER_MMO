using UnityEngine;

public class MonsterIdleState : MonsterStateBase
{
    private float timer;
    public override void Enter()
    {
        base.Enter();
        monster.PlayAnimation(AnimationEvent.Idle);
        timer = monster.mainController.config.maxIdleTime;
    }

    public override void Update()
    {
        base.Update();
        // 追踪玩家
        if (monster.chasePlayer != null && !monster.distanceToPlayerCanAtk)
        {
            monster.ChangeState(MonsterState.chase);
        }
        // 巡逻
        timer -= Time.deltaTime;
        if (timer <= 0) monster.ChangeState(MonsterState.patrol);
        RotateToPlayer();
    }

    private float rotateToPlayerSpeed = 720f;

    private void RotateToPlayer()
    {
        if (monster.chasePlayer == null) return;
        if (!monster.distanceToPlayerCanAtk || monster.angleToPlayerCanAtk) return;

        monster.agent.isStopped = true;

        Vector3 dir = monster.chasePlayer.transform.position - monster.transform.position;
        dir.y = 0f;

        if (dir.sqrMagnitude < 0.0001f) return;

        Quaternion targetRot = Quaternion.LookRotation(dir);
        monster.transform.rotation = Quaternion.RotateTowards(
            monster.transform.rotation,
            targetRot,
            rotateToPlayerSpeed * Time.deltaTime
        );
    }
}