using JKFrame;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtkState : PlayerStateBase
{
    List<PlayerAtkConfig> playerAtkConfigs => player.mainPlayerController.playerAtkConfigs;
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
        currentPlayerAtkConfig = playerAtkConfigs[player.mainPlayerController.playerAtkIndex.Value];
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
    }

    private void OnStopSkillHit()
    {
        player.weaponController.CloseHit();
        player.mainPlayerController.playerAtkIndex.Value++;
        if (player.mainPlayerController.playerAtkIndex.Value >= playerAtkConfigs.Count)
        {
            player.mainPlayerController.playerAtkIndex.Value = 0;
        }
    }

    private void Atk()
    {
        player.PlayAnimation(playerAtkConfigs[player.mainPlayerController.playerAtkIndex.Value].animName);
    }

    public override void Update()
    {
        base.Update();
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
            (int)ServerResSystem.GetItemConfig<WeaponConfig>(player.mainPlayerController.currentWeapon.Value.ToString()).atk,
            atkPos = point
        };
        player.PlayEffectOnClient(point);
        target.BeHit(atkData);
    }
}
