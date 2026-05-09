using System;

public class MonsterView : CharacterViewBase
{
    public event Action monsterAttackAction;
    public event Action monsterDieAction;
    public event Action monsterShootAction;

    public void OnShootStart()
    {
        monsterShootAction?.Invoke();
    }

    public void OnHitStart()
    {
        monsterAttackAction?.Invoke();
    }

    public void OnDie()
    {
        monsterDieAction?.Invoke();
    }
}
