using JKFrame;
using UnityEngine;
using Cinemachine;

public class PlayerManager : SingletonMono<PlayerManager>
{
    public static PlayerController localPlayer;
    [SerializeField] private CinemachineFreeLook cinemachineFreeLook;

    protected override void Awake()
    {
        base.Awake();

        EventSystem.AddTypeEventListener<LocalPlayerEvent>(OnInitLocalPlayer);
        //print("EventSystem.AddTypeEventListener<LocalPlayerEvent>(OnInitLocalPlayer)");
    }

    private void OnInitLocalPlayer(LocalPlayerEvent localPlayerEvent)
    {
        //print("OnInitLocalPlayer");
        localPlayer = localPlayerEvent.localPlayer;
        
        cinemachineFreeLook.transform.position = localPlayer.transform.position;
        cinemachineFreeLook.Follow = localPlayer.camaraFollow;
        cinemachineFreeLook.LookAt = localPlayer.cameraLookPos;

    }
}
