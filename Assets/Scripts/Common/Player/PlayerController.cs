using JKFrame;
using System;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor.Animations;
#endif

// 公共
public partial class PlayerController : NetworkBehaviour
{
    private static Func<string, GameObject> getWeaponFunc;
    public static void SetGetWeaponFunc(Func<string, GameObject> func) {  getWeaponFunc = func; }

    [SerializeField] private PlayerAtkConfigList playerAtkConfigList;
    public List<PlayerAtkConfig> playerAtkConfigs => playerAtkConfigList.playerAtkConfigs;
    public float playerAtkToZeroCd { get; private set; } = 5f;
    public float maxHp;

    public NetVariable<int> playerAtkIndex = new NetVariable<int>(0);
    public NetVariable<PlayerState> currentState = new NetVariable<PlayerState>(PlayerState.None);
    public NetVariable<FixedString32Bytes> currentWeapon = new NetVariable<FixedString32Bytes>("");
    public NetVariable<FixedString32Bytes> playerName = new NetVariable<FixedString32Bytes>("");
    public NetVariable<float> currentHp = new NetVariable<float>(100); 

    public IPlayerServerController playerServerController;
    public IPlayerClientController playerClientController;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        EventSystem.TypeEventTrigger<PlayerSpawnEvent>(new PlayerSpawnEvent() { mainPlayerController = this });
        currentWeapon.OnValueChanged = OnWeaponChanged;
        currentHp.OnValueChanged = OnHpChanged;
    }

    public Action onHpChanged;
    private void OnHpChanged(float previousValue, float newValue)
    {
        onHpChanged?.Invoke();
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
    }

    public Action<GameObject> onWeaponChanged;
    private void OnWeaponChanged(FixedString32Bytes previousValue, FixedString32Bytes newValue)
    {
        GameObject weaponObj = getWeaponFunc?.Invoke(newValue.ToString());
        onWeaponChanged?.Invoke(weaponObj);
    }

    [ServerRpc(RequireOwnership = true)]
    public void Send_InputInfo_ServerRpc(Vector2 dir)
    {
        playerServerController?.MoveOnServer(dir);
    }

    [ServerRpc(RequireOwnership = true)]
    public void Send_Jump_ServerRpc()
    {
        playerServerController?.JumpOnServer();
    }
    [ServerRpc(RequireOwnership = true)]
    public void Send_Atk_ServerRpc()
    {
        playerServerController?.AtkOnServer();
    }

    [ClientRpc]
    public void Send_PlayEffect_ClientRpc(Vector3 point)
    {
        playerClientController.PlaySkillEffect(point);
    }

#if UNITY_EDITOR
    [ContextMenu("自动设置Animator")]
    public void SetAnimatorSettings()
    {
        AnimatorController animatorController = (AnimatorController)GetComponentInChildren<Animator>().runtimeAnimatorController;
        animatorController.parameters = null;
        AnimatorStateMachine stateMachine = animatorController.layers[0].stateMachine;
        stateMachine.anyStateTransitions = null;
        foreach (ChildAnimatorState state in stateMachine.states)
        {
            string triggerName = state.state.name;
            AnimatorControllerParameter animatorControllerParameter = new AnimatorControllerParameter()
            {
                name = triggerName,
                type = AnimatorControllerParameterType.Trigger
            };
            animatorController.AddParameter(animatorControllerParameter);
            AnimatorStateTransition animatorStateTransition = stateMachine.AddAnyStateTransition(state.state);
            animatorStateTransition.AddCondition(AnimatorConditionMode.If, 0, triggerName);
        }
    }
#endif
    //private void OnDrawGizmos()
    //{
    //    if (!drawDetectRange) return;
    //    Gizmos.DrawWireSphere(footTransform.position + Vector3.down * detectOffset, detectRadius);
    //}
}