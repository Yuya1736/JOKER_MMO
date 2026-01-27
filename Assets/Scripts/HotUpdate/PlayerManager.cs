using Cinemachine;
using JKFrame;
using System;
using Unity.Netcode;
using UnityEngine;

public class PlayerManager : SingletonMono<PlayerManager>
{
    public static PlayerController localPlayer;
    public static BagData bagData;
    [SerializeField] private CinemachineFreeLook cinemachineFreeLook;
    public CinemachineFreeLook FreeLook => cinemachineFreeLook;

    public bool IsCompeleted()
    {
        return localPlayer != null;
    }

    public void Init()
    {
        EventSystem.AddTypeEventListener<LocalPlayerEvent>(OnInitLocalPlayer);
        NetMessageManager.Instance.RegisterOnReceiveMessageCallback(NetMessageType.S2C_GetBagData, OnReceiveBagData);
        NetMessageManager.Instance.RegisterOnReceiveMessageCallback(NetMessageType.S2C_UpdateBagData, OnReceiveUpdateBagData);
    }

    private void OnDestroy()
    {
        EventSystem.RemoveTypeEventListener<LocalPlayerEvent>(OnInitLocalPlayer);
        NetMessageManager.Instance.UnRegisterOnReceiveMessageCallback(NetMessageType.S2C_GetBagData, OnReceiveBagData);
        NetMessageManager.Instance.UnRegisterOnReceiveMessageCallback(NetMessageType.S2C_UpdateBagData, OnReceiveUpdateBagData);
    }
    private void OnReceiveBagData(ulong clientId, INetworkSerializable serializable)
    {
        S2C_GetBagData s2C_GetBagData = (S2C_GetBagData)serializable;
        if(s2C_GetBagData.haveBag) bagData = s2C_GetBagData.bagData;
        UISystem.Show<UI_BagWindow>().Show(bagData);
    }

    private void Update()
    {
        //  打开背包
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (UISystem.GetWindow<UI_BagWindow>() == null || !UISystem.GetWindow<UI_BagWindow>().gameObject.activeInHierarchy)
            {
                NetMessageManager.Instance.SendMessageToServer<C2S_GetBagData>(NetMessageType.C2S_GetBagData, new C2S_GetBagData
                {
                    version = bagData == null ? -1 : bagData.version
                });
            }
            else
            {
                UISystem.Close<UI_BagWindow>();
            }
        }
    }

    private void OnInitLocalPlayer(LocalPlayerEvent localPlayerEvent)
    {
        localPlayer = localPlayerEvent.localPlayer;
        
        cinemachineFreeLook.transform.position = localPlayer.transform.position;
        cinemachineFreeLook.Follow = localPlayer.camaraFollow;
        cinemachineFreeLook.LookAt = localPlayer.cameraLookPos;
    }

    public void UseItem(int index)
    {
        print("Send UseItem");
        NetMessageManager.Instance.SendMessageToServer<C2S_UseItem>(NetMessageType.C2S_UseItem, new C2S_UseItem
        {
            index = index
        });
    }

    private void OnReceiveUpdateBagData(ulong clientId, INetworkSerializable serializable)
    {
        print("Receive UpdateItem");
        S2C_UpdateBagData s2C_UpdateBagData = (S2C_UpdateBagData)serializable;
        // 如果版本相同 不需要操作
        if (bagData == null || bagData.version == s2C_UpdateBagData.version) return;
        bagData.version = s2C_UpdateBagData.version;
        // 如果背包窗口存在 更新背包对应物体的表现
        if (UISystem.GetWindow<UI_BagWindow>() != null && UISystem.GetWindow<UI_BagWindow>().gameObject.activeInHierarchy)
        {
            UISystem.GetWindow<UI_BagWindow>().UpdataItem(s2C_UpdateBagData.index, s2C_UpdateBagData.itemData);
        }
        bagData.itemDataList[s2C_UpdateBagData.index] = s2C_UpdateBagData.itemData;
    }
}
