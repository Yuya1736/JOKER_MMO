using JKFrame;
using System;
using System.Threading;
using UnityEngine;

public class BulletManager : Singleton<BulletManager>
{
    public void Init()
    {
        EventSystem.AddTypeEventListener<BulletSpawnEvent>(OnBulletSpawn);
    }

    private void OnBulletSpawn(BulletSpawnEvent @event)
    {
        BulletController mainController = @event.mainBulletController;
        mainController.Init();
        BulletClientController clientController = (BulletClientController)mainController.sideController;
        clientController.Init(mainController);
    }
}