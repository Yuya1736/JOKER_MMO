using JKFrame;
using System;
using Unity.Netcode.Components;
using UnityEngine;

public class PlayerServerController : MonoBehaviour, IPlayerServerController, IStateMachineOwner
{
    public class InputInfo 
    { 
        public Vector2 dir;
        public bool jump;
        public bool atk;
    }
    public InputInfo inputData { get; private set; }
    public StateMachine stateMachine { get; private set; }
    public CharacterController characterController { get; private set; }
    public Animator animator { get; private set; }
    public NetworkAnimator networkAnimator { get; private set; }
    public float verticalVelocity { get; private set; }
     
    public PlayerController mainPlayerController;

    public PlayerView playerView { get; private set; }
    public float speed { get; private set; } = 1f;

    public float airSpeed { get; private set; } = 2f;

    public float jumpHeight { get; private set; } = 2f;

    public WeaponController weaponController { get; private set; }

    [SerializeField, Header("重力系统")] private float gravity = 9.8f;
    [SerializeField] public bool hasGravity { get; private set; } = true;

    [SerializeField] private float maxGravity = 52f;
#pragma warning disable 0414
    [SerializeField] private float CheckFallDeltaTime = 0.25f;
#pragma warning restore  0414
    [SerializeField] private float detectRadius = 0.25f;
    [SerializeField] public bool isGrounded { get; private set; }
    //[SerializeField] private bool drawDetectRange;
    [SerializeField] private float detectOffset = 0f;
    [SerializeField] private Transform footTransform;
    [SerializeField] private LayerMask groundLayer;
    public void Init(PlayerController mainPlayerController)
    {
        groundLayer = LayerMask.GetMask("Ground", "Walkable");

        stateMachine = new StateMachine();
        if (characterController == null) characterController = this.GetComponent<CharacterController>();
        if (playerView == null) playerView = transform.Find("PlayerView").GetComponent<PlayerView>();
        if (animator == null) animator = playerView.GetComponent<Animator>();
        if (networkAnimator == null) networkAnimator = playerView.GetComponent<NetworkAnimator>();
        if (footTransform == null) footTransform = playerView.transform;
        this.mainPlayerController = mainPlayerController;

        mainPlayerController.playerServerController = this;
        mainPlayerController.maxHp = ServerResSystem.serverConfig.maxHp;
        mainPlayerController.onWeaponChanged += playerView.SetWeapon;
        mainPlayerController.onWeaponChanged += MainController_OnWeaponChanged;
        AOIUtility.InitClient(mainPlayerController, AOIUtility.GetChunkCoordByWorldPosition(this.transform.position));
        inputData = new InputInfo();
        stateMachine.Init(this);
        ChangeState(PlayerState.Idle);
        verticalVelocity = 0f;
        this.AddUpdate(SetPlayerGravity);
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

    private void OnDestroy()
    {
        stateMachine.Stop();
        stateMachine.Destroy();
        this.RemoveUpdate(SetPlayerGravity);
    }

    public void PlayEffectOnClient(Vector3 point)
    {
        mainPlayerController.Send_PlayEffect_ClientRpc(point);
    }

    public void MoveOnServer(Vector2 dir)
    {
        inputData.dir = dir.normalized;
    }

    public void JumpOnServer()
    {
        switch (mainPlayerController.currentState.Value)
        {
            case PlayerState.Idle:
            case PlayerState.Move:
                inputData.jump = true;
                break;
        }
    }

    public void AtkOnServer()
    {
        switch (mainPlayerController.currentState.Value)
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
        mainPlayerController.currentState.Value = state;

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
            default:
                break;
        }
    }

    public void PlayAnimation(string animation)
    {
        networkAnimator.SetTrigger(animation);
    }

    public void UpdateClientVisualChunk(Vector2Int oldChunkCoord, Vector2Int newChunkCoord)
    {
        AOIUtility.UpdateClientVisualChunk(mainPlayerController, oldChunkCoord, newChunkCoord);
    }

    public bool GroundedDetect()
    {
        return Physics.CheckSphere(footTransform.position + Vector3.down * detectOffset, detectRadius, groundLayer, QueryTriggerInteraction.Ignore);
    }

    public void SetPlayerGravity()
    {
        isGrounded = GroundedDetect();
        if (isGrounded)
        {
            if (verticalVelocity < 0f) verticalVelocity = 2f;
        }
        else
        {
            if (verticalVelocity < maxGravity)
            {
                verticalVelocity += Time.deltaTime * gravity;
            }
        }
    }

    public void SetHasGravity(bool hasGravity)
    {
        verticalVelocity = 0f;
        this.hasGravity = hasGravity;
    }
}
