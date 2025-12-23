using JKFrame;
using Unity.Netcode;
using UnityEngine;

// 公共
public partial class PlayerController : NetworkBehaviour
{
    public Transform cameraLookPos;
    public Transform camaraFollow;
    

    public NetVariable<PlayerState> currentState = new NetVariable<PlayerState>(PlayerState.None);

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

    //    Debug.Log(
    //$"[Player Spawn] " +
    //$"IsServer={IsServer}, IsClient={IsClient}, IsOwner={IsOwner}, " +
    //$"OwnerId={OwnerClientId}, LocalId={NetworkManager.LocalClientId}"
//);

#if !UNITY_SERVER
        if (IsClient && IsOwner)
        {

            Client_OnNetworkSpawn();
        }
#endif

#if UNITY_SERVER || UNITY_EDITOR
        if (IsServer)
        {
            Server_OnNetworkSpawn();
        }
#endif
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();

#if !UNITY_SERVER
        if (IsClient)
        {
            Client_OnNetworkDespawn();
        }
#endif

#if UNITY_SERVER || UNITY_EDITOR
        if (IsServer)
        {
            Server_OnNetworkDespawn();
        }
#endif
    }

    [ServerRpc(RequireOwnership = false)]
    private void Send_InputInfo_ServerRpc(Vector2 dir)
    {
#if UNITY_SERVER || UNITY_EDITOR
        MovementOnServer(dir);
#endif
    }
}

// 客户端
#if !UNITY_SERVER
public partial class PlayerController : NetworkBehaviour
{
    private Camera mainCamera;
    private void Client_OnNetworkSpawn()
    {
        mainCamera = Camera.main;
        EventSystem.TypeEventTrigger<LocalPlayerEvent>(new LocalPlayerEvent() { localPlayer = this });
        this.AddUpdate(ClientMoveInput);
        AOIUtility.InitClient(this, AOIUtility.GetChunkCoordByWorldPosition(this.transform.position));
    }

    private void Client_OnNetworkDespawn()
    {

    }

    private Vector2 lastDir = Vector2.zero;
    private void ClientMoveInput()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(x, y).normalized;
        if (Vector2.Distance(lastDir, dir) <= 0.01f) return;
        lastDir = dir;
        // 加上摄像机视角旋转角度
        Vector3 dir3 = new Vector3(dir.x, 0, dir.y);
        float yEuler = mainCamera.transform.eulerAngles.y;
        Vector3 newDir3 = Quaternion.Euler(new Vector3(0, yEuler, 0)) * dir3;
        
        Send_InputInfo_ServerRpc(new Vector2(newDir3.x, newDir3.z));
    }
}
#endif

// 服务端
#if UNITY_SERVER || UNITY_EDITOR
public partial class PlayerController : NetworkBehaviour, IStateMachineOwner
{
    [SerializeField] public float speed = 3;
    public float Speed => speed;
    public class InputInfo { public Vector2 dir; }
    public InputInfo inputData { get; private set; }
    public StateMachine stateMachine { get; private set; }
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerView playerView;
    [SerializeField] private CharacterController characterController;
    public Animator Animator => animator;
    public PlayerView PlayerView => playerView;
    public CharacterController CharacterController => characterController;

    [SerializeField, Header("重力系统")] private float gravity = 9.8f;
    [SerializeField] private float maxGravity = 52f;
    [SerializeField] private float CheckFallDeltaTime = 0.25f;
    [SerializeField] private float detectRadius = 0.2f;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool drawDetectRange;
    [SerializeField] private float detectOffset;
    [SerializeField] private Transform footTransform;
    [SerializeField] private LayerMask groundLayer;
    public float verticalVelocity { get; private set; }

    private void Server_OnNetworkSpawn()
    {
        if (playerView == null) playerView = transform.Find("PlayerView").GetComponent<PlayerView>();
        if (animator == null) animator = playerView.GetComponent<Animator>();
        if (characterController == null) characterController = this.GetComponent<CharacterController>();
        if (footTransform == null) footTransform = playerView.transform;

        AOIUtility.InitClient(this, AOIUtility.GetChunkCoordByWorldPosition(this.transform.position));
        inputData = new InputInfo();
        stateMachine = new StateMachine();
        stateMachine.Init(this);
        ChangeState(PlayerState.Idle);
        verticalVelocity = 0f;
        this.AddUpdate(SetPlayerGravity);

    }

    private void Server_OnNetworkDespawn()
    {
        stateMachine.Destroy();
        this.RemoveUpdate(SetPlayerGravity);
    }

    private void MovementOnServer(Vector2 dir)
    {
        inputData.dir = dir.normalized;
    }

    public void ChangeState(PlayerState state)
    {
        //currentState.Value = state;

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
            default:
                break;
        }
    }

    public void PlayAnimation(string animation, float fixedTransitionDuration = 0.2f) 
    {
        animator.CrossFadeInFixedTime(animation, fixedTransitionDuration); 
    }

    public void UpdateClientVisualChunk(Vector2Int oldChunkCoord, Vector2Int newChunkCoord)
    {
        AOIUtility.UpdateClientVisualChunk(this, oldChunkCoord, newChunkCoord);
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
            if (verticalVelocity < 0f) verticalVelocity = -2f;
        }
        else
        {
            if (verticalVelocity < maxGravity)
            {
                verticalVelocity += Time.deltaTime * gravity;
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (!drawDetectRange) return;
        Gizmos.DrawWireSphere(footTransform.position + Vector3.down * detectOffset, detectRadius);
    }

    
}
#endif