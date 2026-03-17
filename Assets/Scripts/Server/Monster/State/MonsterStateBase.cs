using JKFrame;
using UnityEngine;

public class MonsterStateBase : StateBase
{
    public MonsterServerController monster;

    public override void Init(IStateMachineOwner owner)
    {
        base.Init(owner);
        monster = owner as MonsterServerController;
    }
    public override void Update()
    {
        if (!monster.gameObject.activeInHierarchy || stateMachine.currStateObj == null || stateMachine.CurrStateType == null) return;
        base.Update();
    }
}