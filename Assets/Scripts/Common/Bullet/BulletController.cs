using JKFrame;
using Unity.Netcode;
using UnityEngine;

public class BulletController : NetworkEntityBase
{
    public BulletConfig config;
    public IBulletClientController clientController;
    public IBulletServerController serverController;
    [HideInInspector] public Vector3 bulletBoomEffPos;

    public bool isAlive => gameObject.activeInHierarchy && NetworkObject.IsSpawned;

    public override void Init()
    {
        base.Init();
    }

    [ClientRpc]
    public void Send_PlayBoomEffect_ClientRpc()
    {
        clientController.PlayHitEffect(config.boomEffect);
    }

    [ClientRpc]
    public void Send_PlayBoomEffect_ClientRpc(Vector3 point)
    {
        clientController.PlayHitEffect(point);
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        EventSystem.TypeEventTrigger<BulletSpawnEvent>(new BulletSpawnEvent { mainBulletController = this });
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
    }
}