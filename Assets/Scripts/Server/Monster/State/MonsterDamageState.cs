using JKFrame;
using UnityEngine;

public class MonsterDamageState : MonsterStateBase
{
    private AtkData atkData;
    public float repelSpeed => monster.repelSpeed;
    public float repelTime => monster.repelTime;
    public float repelTimer;
    public bool canBeRepel;

    private float exitTime = 0.8f;
    private Coroutine timerCoroutine;
    public override void Enter()
    {
        base.Enter();
        monster.PlayAnimation(AnimationEvent.Damage);
        canBeRepel = true;  
        timerCoroutine = TimerUtils.ExecuteAfterDelay(exitTime, () => { monster.ChangeState(MonsterState.patrol); });
    }

    public void SetAtkData(AtkData data)
    {
        atkData = data;
    }

    public override void Update()
    {
        base.Update();
        UpdateRepelTime();
    }

    public void MonsterBeAtk()
    {
        monster.mainController.ChangeHp(-atkData.atkValue);
        if (!monster.isAlive)
        {
            monster.ChangeState(MonsterState.die);
            return;
        }
        if (canBeRepel)
        {
            Vector3 repelDir = monster.transform.position - atkData.repelSourcePos;
            repelDir.y = 0; // 防止Monter向上击退
            repelDir.Normalize();
            Vector3 motion = repelSpeed * repelDir;
            monster.characterController.Move(motion);
        }
    }

    private void UpdateRepelTime()
    {
        if (canBeRepel) return;
        repelTimer -= Time.deltaTime;
        if (repelTimer <= 0)
        {
            repelTimer = repelTime;
            canBeRepel = true;
        }
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