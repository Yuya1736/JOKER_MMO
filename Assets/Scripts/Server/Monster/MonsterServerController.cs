using JKFrame;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;

public class MonsterServerController : CharacterServerControllerBase<MonsterController>, IMonterServerController, IHitTarget, INetworkSideController
{
    public MonsterSpawner spawner;
    public int spawnIndex;

    public NavMeshAgent agent;
    public CharacterController characterController;
    public MonsterConfig config => mainController.config;

    // 击退
    public float repelSpeed = 1f;
    public float repelTime = 0.8f;

    private LayerMask playerLayerMask;


    public override void Init(MonsterController mainController)
    {
        base.Init(mainController);
        AOIUtility.InitServerObjectVisualChunk(mainController.NetworkObject, AOIUtility.GetChunkCoordByWorldPosition(transform.position));
        characterController = GetComponent<CharacterController>();
        playerLayerMask = LayerMask.GetMask("Player");
        agent = GetComponent<NavMeshAgent>();
        mainController.config = ServerResSystem.GetMonsterConfig(gameObject.name);
        mainController.serverController = this;
        stateMachine = new StateMachine();
        mainController.InitHp();
        stateMachine.Init(this);
        ChangeState(MonsterState.Idle);

        // 对象池复用防重订阅
        mainController.view.monsterDieAction -= OnMonsterDie;
        mainController.view.monsterDieAction += OnMonsterDie;

        mainController.view.monsterShootAction -= OnMonsterShoot;
        mainController.view.monsterShootAction += OnMonsterShoot;

        mainController.view.monsterAttackAction -= OnMonsterAtk;
        mainController.view.monsterAttackAction += OnMonsterAtk;
    }

    

    private void Update()
    {
        UpdateSearchPlayer();
        UpdateAtkPlayer();
        UpdateRecoverHpNoBattle();
        UpdateDieState();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        mainController.view.monsterShootAction -= OnMonsterShoot;
        mainController.view.monsterDieAction -= OnMonsterDie;
        mainController.view.monsterAttackAction -= OnMonsterAtk;
        stateMachine.Stop();
        stateMachine.Destroy();
    }

    public void ChangeState(MonsterState state)
    {
        mainController.currentState.Value = state;
        switch (state)
        {
            case MonsterState.None:
                break;
            case MonsterState.Idle:
                stateMachine.ChangeState<MonsterIdleState>();
                break;
            case MonsterState.patrol:
                stateMachine.ChangeState<MonsterPatrolState>();
                break;
            case MonsterState.chase:
                stateMachine.ChangeState<MonsterChaseState>();
                EnterBattle();
                break;
            case MonsterState.damage:   
                stateMachine.ChangeState<MonsterDamageState>();
                break;
            case MonsterState.attack:
                stateMachine.ChangeState<MonsterAttackState>();
                EnterBattle();
                break;
            case MonsterState.die:
                stateMachine.ChangeState<MonsterDieState>();
                break;
            default:
                break;
        }
    }

    #region 死亡检测
    private void UpdateDieState()
    {
        if(!isAlive && mainController.currentState.Value != MonsterState.die)
        {
            canRecover = false;
            ChangeState(MonsterState.die);
        }
    }

    private void OnMonsterDie()
    {
        spawner.NotifyMonsterDeath(spawnIndex);
    }
    #endregion

    #region 攻击检测

    // 玩家距离检测参数
    private float checkDistanceTime = 0.1f;
    private float checkDistanceTimer;
    private float sqrDistanceToPlayer;
    [HideInInspector] public bool distanceToPlayerCanAtk;

    // 玩家方位检测参数
    private float detectCanAtkTime = 0.1f;
    private float detectCanAtkTimer;
    public float angleCanAtk = 35;
    [HideInInspector] public bool angleToPlayerCanAtk;

    private void UpdateAtkPlayer()
    {
        UpdateAtkState();
        UpdateAngleCanAtk();
        UpdateDistanceTimer();
    }

    private void UpdateAtkState()
    {
        if (distanceToPlayerCanAtk && angleToPlayerCanAtk)
        {
            distanceToPlayerCanAtk = false;
            angleToPlayerCanAtk = false;
            agent.isStopped = true;
            var currState = mainController.currentState.Value;
            if (isAlive && currState != MonsterState.attack && currState != MonsterState.damage && currState != MonsterState.die)
            {
                ChangeState(MonsterState.attack);
            }
        }
    }

    private void UpdateDistanceTimer()
    {
        checkDistanceTimer -= Time.deltaTime;
        if (checkDistanceTimer <= 0)
        {
            checkDistanceTimer = checkDistanceTime;
            if (chasePlayer == null)
            {
                distanceToPlayerCanAtk = false;
                return;
            }
            sqrDistanceToPlayer = (transform.position - chasePlayer.transform.position).sqrMagnitude;
            if (sqrDistanceToPlayer <= config.atkDistance * config.atkDistance) distanceToPlayerCanAtk = true;
            else distanceToPlayerCanAtk = false;
        }
    }

    private void UpdateAngleCanAtk()
    {
        detectCanAtkTimer -= Time.deltaTime;
        if (detectCanAtkTimer <= 0)
        {
            detectCanAtkTimer = detectCanAtkTime;
            if (chasePlayer == null)
            {
                angleToPlayerCanAtk = false;
                return;
            }
            float angle = Vector3.Angle(transform.forward, chasePlayer.transform.position - transform.position);
            if (angle <= angleCanAtk) angleToPlayerCanAtk = true;
            else angleToPlayerCanAtk = false;
        }
    }
    #endregion

    #region 搜索玩家
    private Collider[] colliders = new Collider[10];
    public PlayerServerController chasePlayer;
    private float searchPlayerTimer;

    private void UpdateSearchPlayer()
    {
        searchPlayerTimer -= Time.deltaTime;
        if (searchPlayerTimer <= 0)
        {
            searchPlayerTimer = ServerResSystem.serverConfig.monsterSearchPlayerCd;
            chasePlayer = SearchPlayer();
        }
    }

    private PlayerServerController SearchPlayer()
    {
        int count = Physics.OverlapSphereNonAlloc(transform.position, config.maxChaseDistance, colliders, playerLayerMask, QueryTriggerInteraction.Ignore);
        if (count != 0)
        {
            float curMaxDis = 99999;
            Collider tar = null;
            for(int i = 0;i < count;++ i)
            {
                float curDis = (transform.position - colliders[i].transform.position).sqrMagnitude;
                if (curDis < curMaxDis)
                {
                    tar = colliders[i];
                    curMaxDis = curDis;
                }
            }
            return tar.GetComponent<PlayerServerController>();
        }
        return null;
    }
    #endregion

    #region 脱战回血
    private float recoverHpTimer; // 脱战回血计时器
    private float recoverHpTime = 3f;
    private float recoverHpValue = 10f; // 每次回血的血量

    private float restTimer = restTime; // 脱战休息计时器, 达到一定时间后才会回血
    private const float restTime = 5f;

    private bool canRecover;

    private void EnterBattle()
    {
        canRecover = false;
        restTimer = restTime;
        recoverHpTimer = 0;
    }

    private void UpdateRecoverHpNoBattle()
    {
        UpdateRestTime();
        UpdateRecoverHpTime();
    }

    private void UpdateRestTime()
    {
        var currState = mainController.currentState.Value;
        if (isAlive && (currState == MonsterState.Idle || currState == MonsterState.patrol))
        {
            restTimer -= Time.deltaTime;
            if(restTimer <= 0)
            {
                canRecover = true;
            }
        }
    }

    private void UpdateRecoverHpTime()
    {
        if (!isAlive) return;
        if (!canRecover) return;

        recoverHpTimer -= Time.deltaTime;
        if (recoverHpTimer <= 0)
        {
            recoverHpTimer = recoverHpTime;
            mainController.ChangeHp(recoverHpValue);
        }
    }
    #endregion

    public void NotifyStruckDownTaskSystem(ulong clientId, string monsterName)
    {
        ClientsManager.Instance.OnPlayerKillMonster(clientId, monsterName);
    }

    public void OnMonsterShoot()
    {
        if (chasePlayer == null) return;
        GameObject bullet = mainController.atkEffectConfig.effectPrefab;
        Vector3 dir = (chasePlayer.transform.position - transform.position).normalized;
        NetworkObject obj = NetManager.Instance.SpawnObject(NetManager.ServerClientId, bullet, mainController.view.atkEffTransform.position, Quaternion.LookRotation(dir));
        obj.TryGetComponent(out BulletController bulletController);
        obj.transform.SetParent(null, true);
        bulletController.Init();
        BulletServerController bulletServerController = bulletController.gameObject.GetComponent<BulletServerController>();
        bulletServerController.Init(bulletController, OnHitTarget);
    }

    public Vector3 GetRandomPatrolPosition()
    {
        return spawner.GetRandomPatrolPosition();
    }

    public void BeHit(AtkData atkData)
    {
        if (!isAlive) return;
        ChangeState(MonsterState.damage);
        var state = (MonsterDamageState)stateMachine.currStateObj;
        state.SetAtkData(atkData);
        state.MonsterBeAtk();
        if (!isAlive) NotifyStruckDownTaskSystem(atkData.clientId, gameObject.name);
    }

    private void OnMonsterAtk()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position + transform.forward * config.atkDistance / 2, config.atkDistance / 2, playerLayerMask, QueryTriggerInteraction.Ignore);
        for (int i = 0; i < colliders.Length; i++)
        {
            IHitTarget target = colliders[i].GetComponentInChildren<IHitTarget>();
            if (target != null)
            {
                OnHitTarget(target, Vector3.zero);
            }
        }
    }

    public void OnHitTarget(IHitTarget target, Vector3 point)
    {
        AtkData atkData = new AtkData()
        {
            atkValue = (int)config.atk,
            atkPos = point,
            repelSourcePos = transform.position
        };
        target.BeHit(atkData);
    }

    public void UpdateServerObjectVisualChunk(Vector2Int oldChunkCoord, Vector2Int newChunkCoord)
    {
        AOIUtility.UpdateServerObjectVisualChunk(mainController.NetworkObject, oldChunkCoord, newChunkCoord);
    }
}
