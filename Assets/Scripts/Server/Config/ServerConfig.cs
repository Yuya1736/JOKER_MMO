using JKFrame;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "ServerConfig", menuName = "GenerateConfig/ServerConfig")]
public class ServerConfig : ConfigBase
{
    public GameObject NetworkManagerPrefab;
    public GameObject serverOnGameScenePrefab;

    [Header("角色")]
    public GameObject playerPrefab;
    public Vector3 defaultPlayerBirthPos;
    public float maxHp = 100; // hp为固定值
    public int defaultMoney;

    [Header("怪物")]
    public float patrolRadius = 8f;
    public float monsterSearchPlayerCd = 0.5f;
    public float maxDistanceFromSpawner = 20f;
    public float respawnCd = 30;
    [SerializeField] private string monsterConfigFolderPath;
    public Dictionary<string, MonsterConfig> monsterConfigDic;
    [Header("其他")]
    public Dictionary<string, GameObject> terrainDic;
    [SerializeField] private string terrainsPath;

    public Dictionary<string, ItemConfigBase> itemConfigDic;
    [SerializeField] private string weaponConfigFolderPath;
    [SerializeField] private string materialConfigFolderPath;
    [SerializeField] private string consumableConfigFolderPath;

#if UNITY_EDITOR
    [Button("ImportTerrains")]
    public void ImportTerrainsToDic()
    {
        if (terrainDic == null) terrainDic = new Dictionary<string, GameObject>();
        else terrainDic.Clear();

        string[] files = Directory.GetFiles(terrainsPath);
        foreach (string file in files)
        {
            if (file.EndsWith(".meta")) continue;
            Debug.Log(file);
            GameObject terrainObj = AssetDatabase.LoadAssetAtPath<GameObject>(file);
            terrainDic.Add(terrainObj.name, terrainObj);
        }
        AssetDatabase.Refresh();
    }

    [Button("ImportConfigs")]
    private void InitItemConfigDic()
    {
        if (itemConfigDic == null) itemConfigDic = new Dictionary<string, ItemConfigBase>(50);
        itemConfigDic.Clear();
        monsterConfigDic.Clear();
        InitSingleMonsterConfig(monsterConfigFolderPath);
        InitSingleItemConfig(weaponConfigFolderPath);
        InitSingleItemConfig(materialConfigFolderPath);
        InitSingleItemConfig(consumableConfigFolderPath);
    }

    private void InitSingleMonsterConfig(string path)
    {
        string[] files = Directory.GetFiles(path);
        foreach (string file in files)
        {
            if (file.EndsWith(".meta")) continue;
            MonsterConfig config = AssetDatabase.LoadAssetAtPath<MonsterConfig>(file);
            monsterConfigDic.Add(config.name, config);
        }
    }
    private void InitSingleItemConfig(string path)
    {
        string[] files = Directory.GetFiles(path);
        foreach (string file in files)
        {
            if (file.EndsWith(".meta")) continue;
            ItemConfigBase config = AssetDatabase.LoadAssetAtPath<ItemConfigBase>(file);
            itemConfigDic.Add(config.name, config);
        }
    }
#endif
}
