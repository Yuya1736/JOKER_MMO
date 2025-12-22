using JKFrame;
using UnityEngine;

public class ServerMapManager : SingletonMono<ServerMapManager>
{
    [SerializeField] private MapConfig mapConfig;
    [SerializeField] private int noTestRange = 30; // 40 * 40原 现在中间15 - 25 为10 * 10
    [SerializeField] private bool openTestRange = true;
    public void Init()
    {
        // 一次性加载整个地图
        int width = (int)(mapConfig.mapSize.x / mapConfig.terrainSize);
        int height = (int)(mapConfig.mapSize.y / mapConfig.terrainSize);

        if (openTestRange)
        {
            for (int x = noTestRange / 2; x < width - noTestRange / 2; ++x)
                for (int y = noTestRange / 2; y < height - noTestRange / 2; ++y)
                {
                    Vector2Int terrainCoord = new Vector2Int(x, y) - mapConfig.terrainCoordOffset;
                    string resKey = $"{x}_{y}";
                    ResSystem.InstantiateGameObjectAsync<Terrain>(resKey, (terrain) =>
                    {
                        terrain.enabled = false;

                        terrain.basemapDistance = 100;
                        terrain.heightmapPixelError = 50;
                        terrain.heightmapMaximumLOD = 1;
                        terrain.detailObjectDensity = 0.9f;
                        terrain.treeDistance = 10;
                        terrain.treeCrossFadeLength = 10;
                        terrain.treeMaximumFullLODCount = 10;

                        terrain.gameObject.transform.position = new Vector3(terrainCoord.x * mapConfig.terrainSize, 0, terrainCoord.y * mapConfig.terrainSize);
                    }, transform, null, false);
                }
            return;
        }
        for (int x = 0; x < width; ++x)
            for (int y = 0; y < height; ++y)
            {
                Vector2Int terrainCoord = new Vector2Int(x, y) - mapConfig.terrainCoordOffset;
                string resKey = $"{x}_{y}";
                ResSystem.InstantiateGameObjectAsync<Terrain>(resKey, (terrain) =>
                {
                    terrain.enabled = false;

                    terrain.basemapDistance = 100;
                    terrain.heightmapPixelError = 50;
                    terrain.heightmapMaximumLOD = 1;
                    terrain.detailObjectDensity = 0.9f;
                    terrain.treeDistance = 10;
                    terrain.treeCrossFadeLength = 10;
                    terrain.treeMaximumFullLODCount = 10;

                    terrain.gameObject.transform.position = new Vector3(terrainCoord.x * mapConfig.terrainSize, 0, terrainCoord.y * mapConfig.terrainSize);
                }, transform, null, false);
            }
    }
}
