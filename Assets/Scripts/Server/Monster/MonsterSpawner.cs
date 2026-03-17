using System;
using System.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public struct SpawnInfo
{
    public Transform spawnPoint;
    public GameObject monsterPrefab;
}

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private SpawnInfo[] spawnInfos;
    private float patrolRadius => ServerResSystem.serverConfig.patrolRadius;

    private float reSpawnTime => ServerResSystem.serverConfig.respawnCd;

    private float[] reSpawnTimers; // 对应Monster的重生计时器
    [HideInInspector] public MonsterServerController[] monsters;

    private void Start()
    {
#if UNITY_SERVER || UNITY_EDITOR
        reSpawnTimers = new float[spawnInfos.Length];
        monsters = new MonsterServerController[spawnInfos.Length];
        if (!NetManager.Instance.IsServer) return;
        for (int i = 0; i < spawnInfos.Length; ++i)
        {
            reSpawnTimers[i] = reSpawnTime;
            monsters[i] = SpawnMonster(spawnInfos[i], i);
        }
#endif
    }

    public void NotifyMonsterDeath(int index)
    {
        monsters[index] = null;
        StartCoroutine(ReSpawnMonster(index));
    }

    private IEnumerator ReSpawnMonster(int index)
    {
        yield return new WaitForSeconds(reSpawnTime);
        monsters[index] = SpawnMonster(spawnInfos[index], index);
        monsters[index].mainController.currentHp.Value = monsters[index].mainController.MaxHp;
    }

    private MonsterServerController SpawnMonster(SpawnInfo info, int index)
    {
        Transform point = info.spawnPoint;
        GameObject prefab = info.monsterPrefab;
        NetworkObject obj = NetManager.Instance.SpawnObject(NetManager.ServerClientId, prefab, point.position, point.rotation);
        MonsterController mainController = obj.gameObject.GetComponent<MonsterController>();
        mainController.Init();
        MonsterServerController serverController = (MonsterServerController)mainController.sideController;
        serverController.Init(obj.GetComponent<MonsterController>());
        serverController.spawner = this;
        serverController.spawnIndex = index;
        return serverController;
    }

    public Vector3 GetRandomPatrolPosition()
    {
        Vector3 point = transform.position + new Vector3(UnityEngine.Random.Range(-patrolRadius, patrolRadius), 0, UnityEngine.Random.Range(-patrolRadius, patrolRadius));
        if (NavMesh.SamplePosition(point, out var hitInfo, 10, NavMesh.AllAreas))
        {
            return hitInfo.position;
        }
        else
        {
            return transform.position;
        }
    }
} 
