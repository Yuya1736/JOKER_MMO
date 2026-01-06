using JKFrame;
using System.Collections.Generic;
using UnityEngine;

public class ClientMapManager : SingletonMono<ClientMapManager>
{
    private enum TerrainState
    {
        Enable, Disable, Loading
    }

    public Transform terrainsFolder;

    public class TerrainController
    {
        public Terrain terrain;
        private float destroyTimer;
        private TerrainState state;

        public void Load(Vector2Int coord)
        {
            Vector2Int coordKey = coord + Instance.mapConfig.terrainCoordOffset;
            string resKey = $"{coordKey.x}_{coordKey.y}";
            ResSystem.InstantiateGameObjectAsync<Terrain>(resKey, (terrain) =>
            {
                this.terrain = terrain;

                terrain.basemapDistance = 100;
                terrain.heightmapPixelError = 50;
                terrain.heightmapMaximumLOD = 1;
                terrain.detailObjectDensity = 0.9f;
                terrain.treeDistance = 10;
                terrain.treeCrossFadeLength = 10;
                terrain.treeMaximumFullLODCount = 10;

                terrain.gameObject.transform.position = new Vector3(coord.x * Instance.mapConfig.terrainSize, 0, coord.y * Instance.mapConfig.terrainSize);
                if (state == TerrainState.Disable)
                {
                    terrain.gameObject.SetActive(false);
                }
            }, Instance.terrainsFolder, null, false);
        }

        public void Enable()
        {
            if (state == TerrainState.Disable)
            {
                destroyTimer = 0;
                if(terrain != null)
                {
                    terrain.gameObject.SetActive(true);
                }
                state = TerrainState.Enable;
            }
        }

        public void Disable()
        {
            if (state == TerrainState.Enable)
            {
                if (terrain != null)
                {
                    terrain.gameObject.SetActive(false);
                }
                state = TerrainState.Disable;
            }
        }

        public bool CheckAndDestroy()
        {
            if (state == TerrainState.Disable)
            {
                destroyTimer += Time.deltaTime;
                if (destroyTimer >= Instance.terrainDestroyTime)
                {
                    Destroy();
                    return true;
                }
            }
            return false;
        }

        public void Destroy()
        {
            if(terrain != null) ResSystem.UnloadInstance(terrain.gameObject);
            terrain = null;
            destroyTimer = 0;
            PoolSystem.PushObject(this);
        }
    }

    public MapConfig mapConfig;
    public Camera mainCamera;
    private QuadTree quadTree;
    public float terrainDestroyTime;
    private Plane[] cameraPlanes = new Plane[6];
    private Dictionary<Vector2Int, TerrainController> terrainControllerDic = new Dictionary<Vector2Int, TerrainController>(600);
    private List<Vector2Int> destroyTerrainCoordList = new List<Vector2Int>(200);

    public bool IsCompeleted()
    {
        if (terrainControllerDic.Count == 0) return false;
        foreach (TerrainController item in terrainControllerDic.Values)
        {
            if(item.terrain == null) return false;
        }
        return true;
    }

    public void Init()
    {
        quadTree = new QuadTree(mapConfig);
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if(quadTree == null) return;
        // 获取摄像机视野切片
        cameraPlanes = GeometryUtility.CalculateFrustumPlanes(mainCamera);
        // 初始化显示初始区块
        quadTree?.CheckVisual();
        // 从字典删除掉销毁的区块
        foreach (var item in terrainControllerDic)
        {
            if (item.Value.CheckAndDestroy()) destroyTerrainCoordList.Add(item.Key);
        }
        foreach (var item in destroyTerrainCoordList)
        {
            terrainControllerDic.Remove(item);
        }
        destroyTerrainCoordList.Clear();
    }

    public void EnableTerrain(Vector2Int coord)
    {
        TerrainController terrainController;
        if (!terrainControllerDic.TryGetValue(coord, out terrainController))
        {
            // 如果字典里没有对应的TerrainController，则从对象池拿或新建一个
            terrainController = ResSystem.GetOrNew<TerrainController>();
            terrainController.Load(coord);
            terrainControllerDic.Add(coord, terrainController);
        }
        terrainController.Enable();
    }

    public void DisableTerrain(Vector2Int coord)
    {
        if (terrainControllerDic.TryGetValue(coord, out TerrainController terrainController))
        {
            terrainController.Disable();
        }
    }

    public bool CheckVisual(Bounds bounds)
    {
        // 相机视野内
        // 扩大检测包围盒大小，提前加载视野更远更宽处区块
        bounds.size *= 2;
        if (GeometryUtility.TestPlanesAABB(cameraPlanes, bounds)) return true;

        // 相机视野外
        // 检测包围盒是否是周围3 * 3区块,
        Vector3 center = Camera.main.transform.position;
        Vector3 size = new Vector3(mapConfig.terrainSize * 3, mapConfig.terrainMaxHeigh, mapConfig.terrainSize * 3);
        Bounds playerBounds = new Bounds(center, size);
        return bounds.Intersects(playerBounds);
    }

    public Vector2Int GetCoordByWorldPos(Vector3 pos)
    {
        return new Vector2Int((int)(pos.x / mapConfig.terrainSize), (int)(pos.z / mapConfig.terrainSize));
    }

    public Vector3 GetWorldPosByCoord(Vector2Int coord)
    {
        return new Vector3(coord.x * mapConfig.terrainSize, 0, coord.y * mapConfig.terrainSize);
    }

#if UNITY_EDITOR
    public bool drawGizmos;
    private void OnDrawGizmos()
    {
        if(drawGizmos) quadTree?.Draw();
    }
#endif
}
