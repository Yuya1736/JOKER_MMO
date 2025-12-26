using UnityEngine;
public class ServerResSystem
{
    [SerializeField] public static ServerConfig serverConfig { get; private set; }
    [SerializeField] public static MapConfig mapConfig { get; private set; }
    static ServerResSystem()
    {
        if (serverConfig == null) serverConfig = ServerGlobal.Instance.ServerConfig;
        if (mapConfig == null) mapConfig = ServerGlobal.Instance.MapConfig;
    }

    public static NetManager InsatantialteNetworkManager()
    {
        GameObject obj = GameObject.Instantiate(serverConfig.NetworkManagerPrefab);
        return obj.GetComponent<NetManager>();
    }

    public static GameObject InsatantialteServerOnGameScene()
    {
        GameObject obj = GameObject.Instantiate(serverConfig.serverOnGameScenePrefab);
        return obj;
    }

    public static GameObject InsatantialteTerrain(int x, int y, Transform transform)
    {
        // 这是对应Terrain在Unity中的位置，如20_20实际上在Unity中是原点（0，0）
        Vector2Int terrainCoord = new Vector2Int(x, y) - mapConfig.terrainCoordOffset;
        string resKey = $"{x}_{y}";
        GameObject obj = GameObject.Instantiate(serverConfig.terrainDic[resKey]);
        Terrain terrain = obj.GetComponent<Terrain>();

        terrain.enabled = false;
        terrain.basemapDistance = 100;
        terrain.heightmapPixelError = 50;
        terrain.heightmapMaximumLOD = 1;
        terrain.detailObjectDensity = 0.9f;
        terrain.treeDistance = 10;
        terrain.treeCrossFadeLength = 10;
        terrain.treeMaximumFullLODCount = 10;
        terrain.gameObject.transform.position = new Vector3(terrainCoord.x * mapConfig.terrainSize, 0, terrainCoord.y * mapConfig.terrainSize);

        terrain.transform.SetParent(transform);

        return obj;
    }
}