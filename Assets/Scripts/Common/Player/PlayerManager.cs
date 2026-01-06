using JKFrame;
using UnityEngine;
using Cinemachine;

public class PlayerManager : SingletonMono<PlayerManager>
{
    public static PlayerController localPlayer;
    [SerializeField] private CinemachineFreeLook cinemachineFreeLook;

    public bool IsCompeleted()
    {
        return localPlayer != null;
    }

    public void Init()
    {
        EventSystem.AddTypeEventListener<LocalPlayerEvent>(OnInitLocalPlayer);
    }

    private void OnDestroy()
    {
        EventSystem.RemoveTypeEventListener<LocalPlayerEvent>(OnInitLocalPlayer);
    }

    private void OnInitLocalPlayer(LocalPlayerEvent localPlayerEvent)
    {
        localPlayer = localPlayerEvent.localPlayer;
        
        cinemachineFreeLook.transform.position = localPlayer.transform.position;
        cinemachineFreeLook.Follow = localPlayer.camaraFollow;
        cinemachineFreeLook.LookAt = localPlayer.cameraLookPos;

    }
}
