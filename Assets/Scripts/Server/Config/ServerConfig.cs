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

    public GameObject playerPrefab;
    public Vector3 defaultPlayerBirthPos;

    public Dictionary<string, GameObject> terrainDic;

    [SerializeField] private string terrainsPath;

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
#endif
}
