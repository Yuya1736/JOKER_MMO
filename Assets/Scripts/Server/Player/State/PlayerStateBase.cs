using JKFrame;
using UnityEngine;
public class PlayerStateBase : StateBase
{
    public PlayerServerController player;

    public override void Init(IStateMachineOwner owner)
    {
        base.Init(owner);

        player = owner as PlayerServerController;
    }

    public override void Update()
    {
        if (!player.gameObject.activeInHierarchy || stateMachine.currStateObj == null || stateMachine.CurrStateType == null) return;
        base.Update();
        if (player.hasGravity) player.characterController?.Move(Vector3.down * player.verticalVelocity * Time.deltaTime);
    }

    protected float nowEulerY;
    protected float currentVelocity;
    protected void UpdateTurnDir()
    {
        nowEulerY = player.playerView.transform.eulerAngles.y;
        float tanRad = Mathf.Atan2(player.inputData.dir.x, player.inputData.dir.y);
        float tanDeg = Mathf.Rad2Deg * tanRad;
        nowEulerY = Mathf.SmoothDampAngle(nowEulerY, tanDeg, ref currentVelocity, 0.1f);
        player.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, nowEulerY, player.transform.eulerAngles.z);
    }
}