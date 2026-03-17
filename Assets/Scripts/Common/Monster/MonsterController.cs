using JKFrame;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : CharacterControllerBase<MonsterView, IMonsterClientController, IMonterServerController>
{
    public NetVariable<MonsterState> currentState = new NetVariable<MonsterState>(MonsterState.None);
    public EffectConfig atkEffectConfig; // 目前只有一个攻击特效，如果有多个技能可以改成列表
    [HideInInspector] public MonsterConfig config;
    private NavMeshAgent agent;
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        EventSystem.TypeEventTrigger<MonsterSpawnEvent>(new MonsterSpawnEvent { mainMonsterController = this });
        if (viewBase == null) viewBase = GetComponentInChildren<MonsterView>();
        if (view == null) view = GetComponentInChildren<MonsterView>();
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
            if (IsClient) agent.enabled = false;
            if (IsServer) agent.enabled = true;
        }
        //view.monsterAttackAction += () => { Send_PlayEffect_ClientRpc(atkConfig.effect); };
    }

    public override void InitHp()
    {
        maxHp.Value = config.maxHp;
        currentHp.Value = MaxHp;
    }

    //[ClientRpc]
    //public void Send_PlayEffect_ClientRpc(EffectConfig config)
    //{
    //    clientController.PlaySkillEffect(config);
    //}
}
