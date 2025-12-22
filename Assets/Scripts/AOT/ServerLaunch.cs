using JKFrame;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServerLaunch : MonoBehaviour
{
    private void Start()
    {
        Init();
        DontDestroyOnLoad(gameObject);
    }

    private void Init()
    {
        ResSystem.InstantiateGameObject("ServerGlobal");
        SceneManager.sceneLoaded += onGameSceneLoaded;
        SceneManager.LoadScene("GameScene");
    }

    private void onGameSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        ResSystem.InstantiateGameObject("GameSceneManager");
        SceneManager.sceneLoaded -= onGameSceneLoaded;
    }
}
