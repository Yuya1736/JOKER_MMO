using JKFrame;
using UnityEngine;

public class ClientGlobal : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        Init(); 
    }

    private void Init()
    {
        Application.targetFrameRate = 60;

        NetworkVariableSerializationBinder.Init();

        ResSystem.InstantiateGameObject<NetManager>("NetworkManager").Init(true);

        EventSystem.AddTypeEventListener<GameSceneLaunchEvent>(onGameSceneLaunchEvent);
    }

    private void onGameSceneLaunchEvent(GameSceneLaunchEvent @event)
    {
        ResSystem.InstantiateGameObject("ClientOnGameScene");
    }
}
