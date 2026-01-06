using JKFrame;
using System.Collections;
using UnityEngine;

public class ClientOnGameSceneManager : SingletonMono<ClientOnGameSceneManager>
{
    void Start()
    {
        ClientMapManager.Instance.Init();
        PlayerManager.Instance.Init();
        StartCoroutine(LoadGameScene());
    }

    private float progress = 0f;
    private IEnumerator LoadGameScene()
    {
        UI_LoadingWindow window = UISystem.Show<UI_LoadingWindow>();
        window.SetDescription("Loading");
        window.UpdateProgress(progress, 100);
        NetMessageManager.Instance.SendMessageToServer<C2S_EnterGame>(NetMessageType.C2S_EnterGame, new C2S_EnterGame()); // 向服务端发送进入游戏请求，生成角色

        print(progress);
        yield return new WaitForEndOfFrame();
        while (!ClientMapManager.Instance.IsCompeleted())
        {
            yield return null;
            if (progress < 99) progress += 0.1f;
            window.UpdateProgress(progress, 100);
        }
        progress = 99;
        window.UpdateProgress(progress, 100);
        while(!PlayerManager.Instance.IsCompeleted())
        {
            yield return null;
        }
        progress = 100;
        window.UpdateProgress(progress, 100);
        UISystem.Close<UI_LoadingWindow>();
    }

}
