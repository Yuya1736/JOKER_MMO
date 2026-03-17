using JKFrame;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtkState : PlayerStateBase
{
    List<PlayerAtkConfig> playerAtkConfigs => player.mainController.playerAtkConfigs;
    private bool canSwitch = false;
    public PlayerAtkConfig currentPlayerAtkConfig;
    public override void Enter()
    {
        canSwitch = false;
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
        if (player.inputData.dir == Vector2.zero) player.ChangeState(PlayerState.Idle);
        else player.ChangeState(PlayerState.Move);
    }

    private void OnSkillCanSwitch()
    {
        canSwitch = true;
        TryCombo();
    }

    private void OnStopSkillHit()
    {
        player.weaponController.CloseHit();
        player.mainController.playerAtkIndex.Value++;
        if (player.mainController.playerAtkIndex.Value >= playerAtkConfigs.Count)
        {
            player.mainController.playerAtkIndex.Value = 0;
        }
    }

    private void Atk()
    {
        player.PlayAnimation(playerAtkConfigs[player.mainController.playerAtkIndex.Value].animName);
    }

    public override void Update()
    {
        base.Update();
        TryCombo();
    }

    public void TryCombo()
    {
        if (canSwitch && player.inputData.atk)
        {
            canSwitch = false;
            player.inputData.atk = false;
            Atk();
        }
    }

    public override void Exit()
    {
        player.playerView.StartSkillHitAcion -= OnStartSKillHit;
        player.playerView.StopSkillHitAcion -= OnStopSkillHit;
        player.playerView.SkillCanSwitchAcion -= OnSkillCanSwitch;
        player.playerView.SkillEndAcion -= OnSkillEnd;
    }

    public void OnHitTarget(IHitTarget target, Vector3 point)
    {
        AtkData atkData = new AtkData()
        {
            atkValue = currentPlayerAtkConfig.damage +
            (int)ServerResSystem.GetItemConfig<WeaponConfig>(player.mainController.currentWeapon.Value.ToString()).atk,
            atkPos = point,
            repelSourcePos = player.transform.position
        };
        player.PlayEffectOnClient(point);
        target.BeHit(atkData);
    }
}
