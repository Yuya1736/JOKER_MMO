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
                    ServerResSystem.InsatantialteTerrain(x, y, transform);
                }
            return;
        }
        for (int x = 0; x < width; ++x)
            for (int y = 0; y < height; ++y)
            {
                ServerResSystem.InsatantialteTerrain(x, y, transform);
            }
    }
}
