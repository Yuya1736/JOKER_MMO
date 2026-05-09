using System.Threading;
using UnityEngine;

public class PlayerDamageState : PlayerStateBase
{
    private bool canControl;
    private AtkData atkData;

    public float repelSpeed = 0.5f;
    public float repelTime = 2f;
    public float repelTimer;
    public bool canBeRepel;

    public override void Enter()
    {
        base.Enter();
        player.PlayAnimation(AnimationEvent.Damage);
        canControl = false;
        canBeRepel = true;
        player.playerView.DamageCanControlAction += DamageCanControl;
    }

    public override void Update()
    {
        base.Update();
        if (canControl)
        {
            player.ChangeState(PlayerState.Idle);
        }
        UpdateRepelTime();
    }

    public void SetAtkData(AtkData data)
    {
        atkData = data;
    }

    public override void Exit()
    {
        base.Exit();
        canControl = true;
        player.playerView.DamageCanControlAction -= DamageCanControl;
    }

    private void DamageCanControl()
    {
        canControl = true;
    }

    public void PlayerBeAtk()
    {
        player.mainController.ChangeHp(-atkData.atkValue);
        if (!player.isAlive)
        {
            // TODO: 切换死亡状态
            // player.ChangeState(MonsterState.die);
            Debug.Log("You Die");
            return;
        }
        if (canBeRepel)
        {
            Vector3 repelDir = player.transform.position - atkData.repelSourcePos;
            repelDir.y = 0; // 防止Monter向上击退
            repelDir.Normalize();
            Vector3 motion = repelSpeed * repelDir;
            player.characterController.Move(motion);
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
}