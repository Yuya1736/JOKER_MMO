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
public partial class PlayerController : CharacterControllerBase<PlayerView, IPlayerClientController, IPlayerServerController>
{
    private static Func<string, GameObject> getWeaponFunc;
    public static void SetGetWeaponFunc(Func<string, GameObject> func) {  getWeaponFunc = func; }

    [SerializeField] private PlayerAtkConfigList playerAtkConfigList;
    public List<PlayerAtkConfig> playerAtkConfigs => playerAtkConfigList.playerAtkConfigs;
    public float playerAtkToZeroCd { get; private set; } = 5f;

    public NetVariable<int> playerAtkIndex = new NetVariable<int>(0);
    public NetVariable<PlayerState> currentState = new NetVariable<PlayerState>(PlayerState.None);
    public NetVariable<FixedString32Bytes> currentWeapon = new NetVariable<FixedString32Bytes>("");
    public NetVariable<FixedString32Bytes> playerName = new NetVariable<FixedString32Bytes>("");
    
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();  
        EventSystem.TypeEventTrigger<PlayerSpawnEvent>(new PlayerSpawnEvent() { mainPlayerController = this });
        currentWeapon.OnValueChanged = OnWeaponChanged;
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
        serverController?.MoveOnServer(dir);
    }

    [ServerRpc(RequireOwnership = true)]
    public void Send_Jump_ServerRpc()
    {
        serverController?.JumpOnServer();
    }
    [ServerRpc(RequireOwnership = true)]
    public void Send_Atk_ServerRpc()
    {
        serverController?.AtkOnServer();
    }

    [ClientRpc]
    public void Send_PlayPlayerAtkEffect_ClientRpc(Vector3 point)
    {
        clientController.PlayPlayerAtkEffect(point);
    }

    public override void InitHp()
    {
        maxHp.Value = 100f;
        currentHp.Value = MaxHp;
        //print("GetInstanceID(): " + GetInstanceID());
        //print("NetworkObjectId: " + NetworkObjectId);
        //print("InitHp: " + currentHp.Value);
        //print("------------");
    }


    //private void OnDrawGizmos()
    //{
    //    if (!drawDetectRange) return;
    //    Gizmos.DrawWireSphere(footTransform.position + Vector3.down * detectOffset, detectRadius);
    //}
}