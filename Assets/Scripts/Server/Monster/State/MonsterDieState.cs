using JKFrame;
using UnityEngine;

public class MonsterDieState : MonsterStateBase
{
    public override void Enter()
    {
        base.Enter();
        monster.agent.isStopped = true;
        monster.PlayAnimation(AnimationEvent.Die);
        monster.mainController.view.monsterDieAction -= OnMonsterDie;
        monster.mainController.view.monsterDieAction += OnMonsterDie;
    }

    public override void Exit()
    {
        base.Exit();
        monster.mainController.view.monsterDieAction -= OnMonsterDie;
    }

    public void OnMonsterDie()
    {
        NetManager.Instance.DeSpawnObject(monster.mainController.NetworkObject);
    }
}