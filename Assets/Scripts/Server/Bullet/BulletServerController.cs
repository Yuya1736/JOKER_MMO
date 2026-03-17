using System;
using UnityEngine;

public class BulletServerController : MonoBehaviour, IBulletServerController, INetworkSideController
{
    public BulletController mainController;
    public event Action<IHitTarget, Vector3> onHitTargetAction;
    private int playerLayerIndex;
    private int monsterLayerIndex;

    private float despawnTimer;
    private float despawnTime => mainController.config.despawnTime;

    public void Init(BulletController mainController)
    {
        this.mainController = mainController;
        mainController.serverController = this;
        playerLayerIndex = LayerMask.NameToLayer("Player");
        monsterLayerIndex = LayerMask.NameToLayer("Enemy");
        despawnTimer = despawnTime;
    }

    private void Update()
    {
        if (mainController == null || !mainController.isAlive) return;
        UpdateMove();
        UpdateDespawn();
    }
    public void UpdateMove()
    {
        this.transform.Translate(Vector3.forward * Time.deltaTime * mainController.config.speed);
    }
    private void UpdateDespawn()
    {
        despawnTimer -= Time.deltaTime;
        if (despawnTimer <= 0)
        {
            Despawn();
        }
    }
    private void Despawn()
    {
        if (!mainController.isAlive) return;
        NetManager.Instance.DeSpawnObject(mainController.NetworkObject);
    }

    private void PlayBoomEffectOnClient(Vector3 point)
    {
        mainController.Send_PlayBoomEffect_ClientRpc(point);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == monsterLayerIndex) return;
        Vector3 point = other.ClosestPoint(this.transform.position);
        PlayBoomEffectOnClient(point);
        Despawn();
        if (other.gameObject.layer == playerLayerIndex)
        {
            // TODO: ÉËº¦Íæ¼ÒÂß¼­
        }
    }
}