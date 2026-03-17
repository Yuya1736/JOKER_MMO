using JKFrame;
using Unity.Netcode.Components;
using UnityEngine;

public abstract class CharacterServerControllerBase<M> : MonoBehaviour, ICharacterServerController, IStateMachineOwner where M : CharacterControllerBase
{
    public bool isAlive => mainController.currentHp.Value > 0 && gameObject.activeInHierarchy;
    public M mainController;
    public Animator animator { get; protected set; }
    public NetworkAnimator networkAnimator { get; protected set; }
    public float speed { get; protected set; } = 1f;
    public float verticalVelocity { get; protected set; } = 0f;
    public StateMachine stateMachine { get; protected set; }

    [SerializeField, Header("重力系统")] protected float gravity = 9.8f;
    [SerializeField] public bool hasGravity { get; protected set; } = true;

    [SerializeField] protected float maxGravity = 52f;
#pragma warning disable 0414
    [SerializeField] protected float CheckFallDeltaTime = 0.25f;
#pragma warning restore  0414
    [SerializeField] protected float detectRadius = 0.25f;
    [SerializeField] public bool isGrounded;
    //[SerializeField] private bool drawDetectRange;
    [SerializeField] protected float detectOffset = 0f;
    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] protected Transform footTransform;

    public virtual void Init(M mainController)
    {
        this.mainController = mainController;
        groundLayer = LayerMask.GetMask("Ground", "Walkable");
        if (animator == null)
        {
            animator = mainController.viewBase.GetComponent<Animator>();
            if (animator == null) animator = mainController.GetComponent<Animator>();
        }
        if (networkAnimator == null)
        {
            networkAnimator = mainController.viewBase.GetComponent<NetworkAnimator>();
            if (networkAnimator == null) networkAnimator = mainController.GetComponent<NetworkAnimator>();
        }
        if (footTransform == null)
        {
            footTransform = transform.Find("FootTransform"); 
        }
        AOIUtility.InitClient(mainController, AOIUtility.GetChunkCoordByWorldPosition(this.transform.position));
    }

    public virtual void OnDestroy()
    {

    }

    public void PlayAnimation(string animation)
    {
        networkAnimator.SetTrigger(animation);
    }

    public float GetAnimationProcess()
    {
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        return info.normalizedTime;
    }

    public void UpdateClientVisualChunk(Vector2Int oldChunkCoord, Vector2Int newChunkCoord)
    {
        AOIUtility.UpdateClientVisualChunk(mainController, oldChunkCoord, newChunkCoord);
    }

    public bool GroundedDetect()
    {
        if (footTransform == null)
        {
            Debug.LogError($"{gameObject.name}的 FootTransform 没有设置!!!!!");
        }
        return Physics.CheckSphere(footTransform.position + Vector3.down * detectOffset, detectRadius, groundLayer, QueryTriggerInteraction.Ignore);
    }

    public void SetGravity()
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