using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerOnGameSceneManager : MonoBehaviour
{
    private void Start()
    {
        ClientsManager.Instance.Init();
        ServerGlobal.Instance.Init();
        ServerMapManager.Instance.Init();
        DataBaseManager.Instance.Init();
    }
}
