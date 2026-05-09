using JKFrame;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

// 将 UnityEditor 的引用包裹在宏定义中
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "MapNavMeshConfig", menuName = "GenerateConfig/MapNavMeshConfig")]
public class MapNavMeshConfig : ConfigBase // 注意：要保存字典数据，ConfigBase 底层最好继承自 Odin 的 SerializedScriptableObject
{
    [SerializeField, FolderPath(RequireExistingPath = true)] // 使用 Odin 的特性，可以在面板上直接选文件夹
    private string mapNavMeshDataPath = "Assets/";
    public const string NavMeshKeyPrefix = "NavMesh-";

    [ShowInInspector] // 让 Odin Inspector 在面板上绘制这个字典
    public Dictionary<string, NavMeshData> navMeshDataDic = new Dictionary<string, NavMeshData>();

    // 仅在编辑器下编译导入逻辑
#if UNITY_EDITOR
    [Button("ImportConfigs", ButtonSizes.Medium)]
    private void InitSingleItemConfig()
    {
        if (string.IsNullOrEmpty(mapNavMeshDataPath) || !Directory.Exists(mapNavMeshDataPath))
        {
            Debug.LogError($"[MapNavMeshConfig] 路径不存在或为空: {mapNavMeshDataPath}");
            return;
        }

        // 1. 初始化字典，并清空上一次导入的历史数据
        if (navMeshDataDic == null)
        {
            navMeshDataDic = new Dictionary<string, NavMeshData>();
        }
        navMeshDataDic.Clear();

        string[] files = Directory.GetFiles(mapNavMeshDataPath);
        Debug.Log($"[MapNavMeshConfig] 在目录下找到 {files.Length} 个文件");

        foreach (string file in files)
        {
            // 2. 跨平台安全地获取文件名（带后缀）
            string fileName = Path.GetFileName(file);

            // 忽略 .meta 文件以及不以 NavMesh 开头的文件
            if (fileName.EndsWith(".meta") || !fileName.StartsWith("NavMesh")) continue;

            // 3. 将路径转换为 AssetDatabase 需要的统一正斜杠格式
            string assetPath = file.Replace("\\", "/");

            NavMeshData data = AssetDatabase.LoadAssetAtPath<NavMeshData>(assetPath);

            // 4. 防御性编程：检查是否加载成功，以及字典中是否已存在该 Key
            if (data != null)
            {
                if (!navMeshDataDic.ContainsKey(data.name))
                {
                    navMeshDataDic.Add(data.name, data);
                }
                else
                {
                    Debug.LogWarning($"[MapNavMeshConfig] 发现重名数据，已跳过: {data.name}");
                }
            }
        }

        Debug.Log($"[MapNavMeshConfig] 导入完成！共导入 {navMeshDataDic.Count} 个 NavMesh 数据。");

        // 5. 标记此 ScriptableObject 为脏数据，确保编辑器能把变更的字典数据保存到本地
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
    }
#endif
}