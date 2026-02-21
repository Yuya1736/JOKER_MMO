using UnityEngine;
using UnityEngine.SceneManagement;

public class ServerLaunch : MonoBehaviour
{
    private void Start()
    {
        Init();
        SceneManager.LoadScene("GameScene");
    }

    private void Init()
    {
        ServerResSystem.InsatantialteNetworkManager().Init(false);
    }
}
