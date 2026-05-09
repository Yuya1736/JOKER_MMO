using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AddressableAssets;

public static class ClientMapManagerEditor
{
    [MenuItem("Tools/Map/Load All Terrain Chunks")]
    private static void LoadAllTerrainChunks()
    {
        if (!TryGetSelectedMapManager(out Component mapManager, out Type mapManagerType)) return;

        object mapConfig = GetFieldValue(mapManagerType, mapManager, "mapConfig");
        if (mapConfig == null)
        {
            Debug.LogError("mapConfig is null.");
            return;
        }

        Type mapConfigType = mapConfig.GetType();
        Vector2Int mapSize = (Vector2Int)GetFieldValue(mapConfigType, mapConfig, "mapSize");
        Vector2Int terrainCoordOffset = (Vector2Int)GetFieldValue(mapConfigType, mapConfig, "terrainCoordOffset");
        float terrainSize = (float)GetFieldValue(mapConfigType, mapConfig, "terrainSize");

        Transform terrainsFolder = (Transform)GetFieldValue(mapManagerType, mapManager, "terrainsFolder");
        if (terrainsFolder == null)
        {
            GameObject folder = new GameObject("Terrains");
            Undo.RegisterCreatedObjectUndo(folder, "Create Terrains Folder");
            terrainsFolder = folder.transform;
            SetFieldValue(mapManagerType, mapManager, "terrainsFolder", terrainsFolder);
        }

        int countX = Mathf.FloorToInt(mapSize.x / terrainSize);
        int countY = Mathf.FloorToInt(mapSize.y / terrainSize);
        int startCoordX = -terrainCoordOffset.x;
        int startCoordY = -terrainCoordOffset.y;

        int loadedCount = 0;
        for (int x = 0; x < countX; x++)
        {
            int coordX = startCoordX + x;
            for (int y = 0; y < countY; y++)
            {
                int coordY = startCoordY + y;
                Vector2Int coord = new Vector2Int(coordX, coordY);
                Vector2Int coordKey = coord + terrainCoordOffset;
                string resKey = $"{coordKey.x}_{coordKey.y}";

                if (terrainsFolder.Find(resKey) != null) continue;

                GameObject terrainGO = LoadTerrainByKey(resKey, terrainsFolder);
                if (terrainGO == null) continue;

                Undo.RegisterCreatedObjectUndo(terrainGO, "Load Terrain Chunk");
                terrainGO.transform.position = new Vector3(coord.x * terrainSize, 0, coord.y * terrainSize);

                Terrain terrain = terrainGO.GetComponent<Terrain>();
                if (terrain != null)
                {
                    terrain.basemapDistance = 100;
                    terrain.heightmapPixelError = 50;
                    terrain.heightmapMaximumLOD = 1;
                    terrain.detailObjectDensity = 0.9f;
                    terrain.treeDistance = 10;
                    terrain.treeCrossFadeLength = 10;
                    terrain.treeMaximumFullLODCount = 10;
                }

                loadedCount++;
            }
        }

        EditorUtility.SetDirty(mapManager.gameObject);
        EditorSceneManager.MarkSceneDirty(mapManager.gameObject.scene);
        Debug.Log($"Editor terrain load completed. Loaded chunks: {loadedCount}");
    }

    [MenuItem("Tools/Map/Clear Loaded Terrain Chunks")]
    private static void ClearLoadedTerrainChunks()
    {
        if (!TryGetSelectedMapManager(out Component mapManager, out Type mapManagerType)) return;

        Transform terrainsFolder = (Transform)GetFieldValue(mapManagerType, mapManager, "terrainsFolder");
        if (terrainsFolder == null) return;

        for (int i = terrainsFolder.childCount - 1; i >= 0; i--)
        {
            Transform child = terrainsFolder.GetChild(i);
            Undo.DestroyObjectImmediate(child.gameObject);
        }

        EditorSceneManager.MarkSceneDirty(mapManager.gameObject.scene);
    }

    private static bool TryGetSelectedMapManager(out Component mapManager, out Type mapManagerType)
    {
        mapManager = null;
        mapManagerType = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .FirstOrDefault(t => t.Name == "ClientMapManager");

        if (mapManagerType == null)
        {
            Debug.LogError("Cannot find type ClientMapManager.");
            return false;
        }

        if (Selection.activeGameObject == null)
        {
            Debug.LogError("Please select GameObject that has ClientMapManager component.");
            return false;
        }

        mapManager = Selection.activeGameObject.GetComponent(mapManagerType);
        if (mapManager == null)
        {
            Debug.LogError("Selected GameObject has no ClientMapManager component.");
            return false;
        }

        return true;
    }

    private static object GetFieldValue(Type type, object instance, string fieldName)
    {
        FieldInfo fieldInfo = type.GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        return fieldInfo?.GetValue(instance);
    }

    private static void SetFieldValue(Type type, object instance, string fieldName, object value)
    {
        FieldInfo fieldInfo = type.GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        fieldInfo?.SetValue(instance, value);
    }

    private static GameObject LoadTerrainByKey(string resKey, Transform parent)
    {
#if ENABLE_ADDRESSABLES
        try
        {
            GameObject go = Addressables.InstantiateAsync(resKey, parent).WaitForCompletion();
            if (go != null)
            {
                go.name = resKey;
                return go;
            }
        }
        catch
        {
            // ignore, fallback below
        }
#endif

        GameObject prefab = Resources.Load<GameObject>(resKey);
        if (prefab != null)
        {
            GameObject go = (GameObject)PrefabUtility.InstantiatePrefab(prefab, parent);
            if (go != null) go.name = resKey;
            return go;
        }

        return null;
    }
}
