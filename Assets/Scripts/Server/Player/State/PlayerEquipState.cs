using UnityEngine;

public class PlayerEquipState : PlayerStateBase
{
    private bool _isEquipped;
    public override void Enter()
    {
        base.Enter();
        player.PlayAnimation(AnimationEvent.Equip);
        _isEquipped = false;
        player.playerView.EquipCanControlAction += EquipCanControl;
        player.playerView.EquipEndAction += EquipEnd;
    }

    public override void Update()
    {
        base.Update();
        if(_isEquipped)
        {
            if (player.inputData.atk)
            {
                player.ChangeState(PlayerState.Atk);
                player.inputData.atk = false;
                return;
            }
            if (player.inputData.jump)
            {
                player.ChangeState(PlayerState.Jump);
                player.inputData.jump = false;
                return;
            }
            if (player.inputData.dir != Vector2.zero)
            {
                player.ChangeState(PlayerState.Move);
                return;
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.playerView.EquipCanControlAction -= EquipCanControl;
        player.playerView.EquipEndAction -= EquipEnd;
    }

    private void EquipEnd()
    {
        player.ChangeState(PlayerState.Idle);
    }

    private void EquipCanControl()
    {
        _isEquipped = true;
    }
}