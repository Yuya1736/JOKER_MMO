using JKFrame;
using System.Collections;
using UnityEngine;

public class MonsterPatrolState : MonsterStateBase
{
    private float timer;
    private Vector3 targerPos;

    private Coroutine AOICoroutine;

    public override void Enter()
    {
        base.Enter();
        monster.PlayAnimation(AnimationEvent.Move);
        timer = monster.mainController.config.maxPatrolTime;
        targerPos = monster.GetRandomPatrolPosition();
        monster.agent.SetDestination(targerPos);
        monster.agent.isStopped = false;
        // 开始AOI检测协程
        AOICoroutine = monster.StartCoroutine(CheckAndUpdateAOI());
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

    public override void Exit()
    {
        base.Exit();
        // 关闭AOI检测协程
        monster.StopCoroutine(AOICoroutine);
    }

    WaitForSeconds waitOneSecond = new WaitForSeconds(1f);
    public Vector2Int oldChunkCoord; // 上一次进行AOI检测时的Pos
    public IEnumerator CheckAndUpdateAOI()
    {
        while (true)
        {
            yield return waitOneSecond;
            Vector2Int newChunkCoord = AOIUtility.GetChunkCoordByWorldPosition(monster.transform.position);
            if (oldChunkCoord != newChunkCoord)
            {
                monster.UpdateServerObjectVisualChunk(oldChunkCoord, newChunkCoord);
                oldChunkCoord = newChunkCoord;
            }
        }

    }
}