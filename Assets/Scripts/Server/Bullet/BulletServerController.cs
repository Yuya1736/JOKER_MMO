using System;
using System.Collections;
using System.Threading;
using UnityEngine;

public class BulletServerController : MonoBehaviour, IBulletServerController, INetworkSideController
{
    public BulletController mainController;
    //public event Action<IHitTarget, Vector3> onHitTargetAction;
    private int playerLayerIndex;
    private int monsterLayerIndex;

    private float despawnTimer;
    private float despawnTime => mainController.config.despawnTime;

    public event Action<IHitTarget, Vector3> onHitTargetAction;

    private Coroutine AOICoroutine;
    public void Init(BulletController mainController, Action<IHitTarget, Vector3> onHitTargetAction)
    {
        this.mainController = mainController;
        this.onHitTargetAction = onHitTargetAction;
        AOIUtility.InitServerObjectVisualChunk(mainController.NetworkObject, AOIUtility.GetChunkCoordByWorldPosition(mainController.transform.position));
        mainController.serverController = this;
        playerLayerIndex = LayerMask.NameToLayer("Player");
        monsterLayerIndex = LayerMask.NameToLayer("Enemy");
        despawnTimer = despawnTime;
        AOICoroutine = StartCoroutine(CheckAndUpdateAOI());
    }

    private void OnDestroy()
    {
        onHitTargetAction = null;
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
        StopCoroutine(AOICoroutine);
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
        IHitTarget target = other.GetComponent<IHitTarget>();
        if (target != null && other.gameObject.layer == playerLayerIndex)
        {
            // TODO: 伤害玩家逻辑
            onHitTargetAction?.Invoke(target, point);
        }
    }

    WaitForSeconds waitOneSecond = new WaitForSeconds(1f);
    public Vector2Int oldChunkCoord; // 上一次进行AOI检测时的Pos
    public IEnumerator CheckAndUpdateAOI()
    {
        while (true)
        {
            yield return waitOneSecond;
            Vector2Int newChunkCoord = AOIUtility.GetChunkCoordByWorldPosition(transform.position);
            if (oldChunkCoord != newChunkCoord)
            {
                UpdateServerObjectVisualChunk(oldChunkCoord, newChunkCoord);
                oldChunkCoord = newChunkCoord;
            }
        }

    }

    public void UpdateServerObjectVisualChunk(Vector2Int oldChunkCoord, Vector2Int newChunkCoord)
    {
        AOIUtility.UpdateServerObjectVisualChunk(mainController.NetworkObject, oldChunkCoord, newChunkCoord);
    }
}