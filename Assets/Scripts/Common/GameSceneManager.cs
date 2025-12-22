using JKFrame;
using UnityEngine;
using UnityEngine.Scripting;

[Preserve]
public class GameSceneManager : MonoBehaviour
{
    private void Start()
    {
        if (NetManager.Instance.IsServer)
        {
            ResSystem.InstantiateGameObject("ServerOnGameScene");
        }

        if (NetManager.Instance.IsClient)
        {
            ResSystem.InstantiateGameObject("ClientOnGameScene");
        }
    }
}
