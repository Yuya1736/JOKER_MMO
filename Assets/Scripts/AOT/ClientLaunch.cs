using UnityEngine;
using UnityEngine.SceneManagement;
using JKFrame;
using System;

public class ClientLaunch : MonoBehaviour
{
    void Start()
    {
        Init();
    }

    private void Init()
    {
        print(Application.persistentDataPath);
        // 热更新成功后调用事件
        this.GetComponent<HotUpdateSystem>().StartHotUpdate(null, (succeed) =>
        {
            if (succeed)
            {
                OnHotUpdateSucceed();
            }
        });
    }

    private void OnHotUpdateSucceed()
    {
        ResSystem.InstantiateGameObject("ClientGlobal");
        SceneManager.sceneLoaded += onGameSceneLoaded;
        SceneManager.LoadScene("GameScene");
    }

    private void onGameSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        ResSystem.InstantiateGameObject("GameSceneManager");
        SceneManager.sceneLoaded -= onGameSceneLoaded;
    }
}
