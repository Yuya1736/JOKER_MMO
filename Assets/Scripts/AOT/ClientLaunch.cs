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
        SceneManager.LoadScene("GameScene");
    }
}
