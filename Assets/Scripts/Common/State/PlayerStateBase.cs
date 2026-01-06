#if UNITY_SERVER || UNITY_EDITOR
using JKFrame;
using UnityEngine;
public class PlayerStateBase : StateBase
{
    public PlayerController player;

    public override void Init(IStateMachineOwner owner)
    {
        base.Init(owner);

        player = owner as PlayerController;
    }

    public override void Update()
    {
        if (stateMachine.currStateObj == null || stateMachine.CurrStateType == null) return;
        base.Update();
        player.CharacterController.Move(Vector3.down * player.verticalVelocity * Time.deltaTime);
    }
}
#endif