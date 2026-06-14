using System.Collections.Generic;
using UnityEngine;

public class PlayerAtkState : PlayerStateBase
{
    List<PlayerAtkConfig> playerAtkConfigs => player.mainController.playerAtkConfigs;
    private bool canSwitch = false;
    private bool pendingCombo = false;
    public PlayerAtkConfig currentPlayerAtkConfig;
    public override void Enter()
    {
        canSwitch = false;
        pendingCombo = false;
        Atk();
        player.playerView.StartSkillHitAcion += OnStartSKillHit;
        player.playerView.StopSkillHitAcion += OnStopSkillHit;
        player.playerView.SkillCanSwitchAcion += OnSkillCanSwitch;
        player.playerView.SkillEndAcion += OnSkillEnd;
    }

    private void OnStartSKillHit()
    {
        currentPlayerAtkConfig = playerAtkConfigs[player.mainController.playerAtkIndex.Value];
        player.weaponController.StartHit();
    }

    private void OnSkillEnd()
    {
        if (pendingCombo)
        {
            return;
        }

        if (player.inputData.dir == Vector2.zero) player.ChangeState(PlayerState.Idle);
        else player.ChangeState(PlayerState.Move);
    }

    private void OnSkillCanSwitch()
    {
        canSwitch = true;
        if (player.inputData.comboAtk)
        {
            pendingCombo = true;
            player.inputData.comboAtk = false;
            player.mainController.playerAtkIndex.Value++;
            if (player.mainController.playerAtkIndex.Value >= playerAtkConfigs.Count)
            {
                player.mainController.playerAtkIndex.Value = 0;
            }
            Atk();
            pendingCombo = false;
            canSwitch = false;
        }
    }

    private void OnStopSkillHit()
    {
        player.weaponController.CloseHit();
    }

    private void Atk()
    {
        player.PlayAnimation(playerAtkConfigs[player.mainController.playerAtkIndex.Value].animName);
    }

    public override void Update()
    {
        base.Update();
        TryMove();
    }

    public void TryMove()
    {
        if (canSwitch && player.inputData.sprint && player.inputData.dir != Vector2.zero)
        {
            player.inputData.comboAtk = false;
            player.ChangeState(PlayerState.Move);
        }
    }

    public override void Exit()
    {
        player.playerView.StartSkillHitAcion -= OnStartSKillHit;
        player.playerView.StopSkillHitAcion -= OnStopSkillHit;
        player.playerView.SkillCanSwitchAcion -= OnSkillCanSwitch;
        player.playerView.SkillEndAcion -= OnSkillEnd;
        canSwitch = true;
        pendingCombo = false;
    }

    public void OnHitTarget(IHitTarget target, Vector3 point)
    {
        AtkData atkData = new AtkData()
        {
            clientId = player.mainController.OwnerClientId,
            atkValue = currentPlayerAtkConfig.damage +
            (int)ServerResSystem.GetItemConfig<WeaponConfig>(player.mainController.currentWeapon.Value.ToString()).atk,
            atkPos = point,
            repelSourcePos = player.transform.position
        };
        player.PlayEffectOnClient(point);
        target.BeHit(atkData);
    }
}
