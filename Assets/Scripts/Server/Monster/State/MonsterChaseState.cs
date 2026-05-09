using JKFrame;
using System.Collections;
using UnityEngine;

public class MonsterChaseState : MonsterStateBase
{
    public PlayerServerController chasePlayer;
    private float timer;

    private Coroutine AOICoroutine;


    // 脱离时间参数
    private float checkFarTime = 0.2f;
    private float checkFarTimer;
    private bool isFarFromSpawner;
    private bool isFarFromPlayer;
    

    public override void Enter()
    {
        base.Enter();
        chasePlayer = monster.chasePlayer;
        timer = monster.config.maxChaseTime;
        monster.PlayAnimation(AnimationEvent.Move);
        monster.agent.SetDestination(chasePlayer.transform.position);
        monster.agent.isStopped = false;
        // 开启AOI检测协程
        AOICoroutine = monster.StartCoroutine(CheckAndUpdateAOI());
    }

    public override void Update()
    {
        base.Update();
        UpdateCheckFarTimer();
        /// 退出条件：
        /// 1. 追踪玩家死亡
        /// 2. 超出范围外最大追踪时间
        /// 3. 超出出生点最大范围
        if (isFarFromPlayer) timer -= Time.deltaTime;
        else timer = monster.config.maxChaseTime;

        if (!chasePlayer.isAlive || timer <= 0 || isFarFromSpawner) // 脱离战斗
        {
            monster.agent.isStopped = true;
            monster.ChangeState(MonsterState.patrol);
            return;
        }
        else
        {
            monster.agent.isStopped = false;
            monster.agent.SetDestination(chasePlayer.transform.position);
        }

        RotateToPlayer();
    }

    private void UpdateCheckFarTimer()
    {
        checkFarTimer -= Time.deltaTime;
        if (checkFarTimer <= 0)
        {
            checkFarTimer = checkFarTime;
            CheckisFarFromSpawner();
            CheckIsFarFromPlayer();
        }
    }

    private float rotateToPlayerSpeed = 720f;

    private void RotateToPlayer()
    {
        if (chasePlayer == null) return;
        if (!monster.distanceToPlayerCanAtk || monster.angleToPlayerCanAtk) return;

        monster.agent.isStopped = true;

        Vector3 dir = chasePlayer.transform.position - monster.transform.position;
        dir.y = 0f;

        if (dir.sqrMagnitude < 0.0001f) return;

        Quaternion targetRot = Quaternion.LookRotation(dir);
        monster.transform.rotation = Quaternion.RotateTowards(
            monster.transform.rotation,
            targetRot,
            rotateToPlayerSpeed * Time.deltaTime
        );
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

    private void CheckisFarFromSpawner()
    {
        isFarFromSpawner = (monster.transform.position - monster.spawner.transform.position).sqrMagnitude > Mathf.Pow(ServerResSystem.serverConfig.maxDistanceFromSpawner, 2);
    }

    private void CheckIsFarFromPlayer()
    {
        isFarFromPlayer = (monster.transform.position - chasePlayer.transform.position).sqrMagnitude > Mathf.Pow(monster.config.maxChaseDistance, 2);
    }

    public override void Exit() 
    { 
        base.Exit();
        // 关闭AOI检测协程
        monster.StopCoroutine(AOICoroutine);
    }
}