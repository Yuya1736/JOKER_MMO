using JKFrame;
using System.Collections;
using Unity.Netcode;
using UnityEngine;


public class GameSceneManager : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Init());
    }

    private IEnumerator Init()
    {
        while (NetworkManager.Singleton == null) yield return new WaitForEndOfFrame();

        EventSystem.TypeEventTrigger<GameSceneLaunchEvent>(default);
    }
}
