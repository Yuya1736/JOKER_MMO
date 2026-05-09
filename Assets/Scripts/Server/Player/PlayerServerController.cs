using JKFrame;
using Unity.Netcode;
using UnityEngine;

public class PlayerServerController : CharacterServerControllerBase<PlayerController>, IPlayerServerController, INetworkSideController, IHitTarget
{
    public class InputInfo
    {
        public Vector2 dir;
        public bool jump;
        public bool atk;
    }
    public InputInfo inputData { get; private set; }

    public CharacterController characterController { get; private set; }

    public PlayerView playerView { get; private set; }

    public float airSpeed { get; private set; } = 2f;

    public float jumpHeight { get; private set; } = 2f;

    public WeaponController weaponController { get; private set; }
    

    public override void Init(PlayerController mainPlayerController)
    {
        base.Init(mainPlayerController);
        AOIUtility.InitClientVisualChunk(mainController.OwnerClientId, AOIUtility.GetChunkCoordByWorldPosition(this.transform.position));

        stateMachine = new StateMachine();
        if (characterController == null) characterController = this.GetComponent<CharacterController>();
        if (playerView == null) playerView = transform.Find("PlayerView").GetComponent<PlayerView>();
        if (footTransform == null) footTransform = playerView.transform;

        mainPlayerController.InitHp();

        //mainPlayerController.maxHp.Value = ServerResSystem.serverConfig.maxHp;
        //mainController.currentHp.Value = mainPlayerController.maxHp.Value;

        mainController.serverController = this;

        mainPlayerController.onWeaponChanged += playerView.SetWeapon;
        mainPlayerController.onWeaponChanged += MainController_OnWeaponChanged;

        inputData = new InputInfo();
        stateMachine.Init(this);
        ChangeState(PlayerState.Idle);
        this.AddUpdate(SetGravity);

    }

    private void MainController_OnWeaponChanged(GameObject weapon)
    {
        if (weapon == null) return;
        if (!weapon.TryGetComponent<WeaponController>(out WeaponController weaponController))
        {
            weaponController = weapon.AddComponent<WeaponController>();
        }
        this.weaponController = weaponController;
        weaponController.Init(WeaponController_OnHitTargetAction);
        ChangeState(PlayerState.Equip);
    }

    private void WeaponController_OnHitTargetAction(IHitTarget target, Vector3 point)
    {
        StateMachine stateMachine = this.stateMachine;
        PlayerAtkState playerAtkState = (PlayerAtkState)stateMachine.currStateObj;
        playerAtkState.OnHitTarget(target, point);

        // 因为技能可能有多个击打点，所以每次击中都更新击打点位置，确保特效能正确显示
        EffectConfig effectConfig = playerAtkState.currentPlayerAtkConfig.hitEffectConfig;
        effectConfig.position = point;
    }

    public override void OnDestroy()
    {
        stateMachine.Stop();
        stateMachine.Destroy();
        this.RemoveUpdate(SetGravity);
    }

    public void PlayEffectOnClient(Vector3 point)
    {
        mainController.Send_PlayPlayerAtkEffect_ClientRpc(point);
    }

    public void MoveOnServer(Vector2 dir)
    {
        inputData.dir = dir.normalized;
    }

    public void JumpOnServer()
    {
        switch (mainController.currentState.Value)
        {
            case PlayerState.Idle:
            case PlayerState.Move:
                inputData.jump = true;
                break;
        }
    }

    public void AtkOnServer()
    {
        switch (mainController.currentState.Value)
        {
            case PlayerState.Idle:
            case PlayerState.Move:
            case PlayerState.Atk:
                inputData.atk = true;
                break;
        }
    }

    public void ChangeState(PlayerState state)
    {
        mainController.currentState.Value = state;

        switch (state)
        {
            case PlayerState.None:
                break;
            case PlayerState.Idle:
                stateMachine.ChangeState<PlayerIdleState>();
                break;
            case PlayerState.Move:
                stateMachine.ChangeState<PlayerMoveState>();
                break;
            case PlayerState.Jump:
                stateMachine.ChangeState<PlayerJumpState>();
                break;
            case PlayerState.AirDown:
                stateMachine.ChangeState<PlayerAirDownState>();
                break;
            case PlayerState.Atk:
                stateMachine.ChangeState<PlayerAtkState>();
                break;
            case PlayerState.Damage:
                stateMachine.ChangeState<PlayerDamageState>();
                break;
            case PlayerState.Equip:
                stateMachine.ChangeState<PlayerEquipState>();
                break;
            default:
                break;
        }
    }

    public void UpdateClientVisualChunk(Vector2Int oldChunkCoord, Vector2Int newChunkCoord)
    {
        AOIUtility.UpdateClientVisualChunk(mainController.OwnerClientId, oldChunkCoord, newChunkCoord);
    }

    public void BeHit(AtkData atkData)
    {
        if (!isAlive) return;
        ChangeState(PlayerState.Damage);
        var state = (PlayerDamageState)stateMachine.currStateObj;
        state.SetAtkData(atkData);
        state.PlayerBeAtk(); 
    }
}
