using System;
using UnityEngine;

public class QuadTree
{
    public static MapConfig mapConfig { get; private set; }

    public class Node
    {
        private Bounds bounds;

        private Node LeftAndTop;
        private Node RightAndTop;
        private Node LeftAndBottom;
        private Node RightAndBottom;

        private bool isTerrain;
        private Vector2Int terrainCoord;

        public Node(Bounds bounds, bool isDivide)
        {
            this.bounds = bounds;
            isTerrain = CheckTerrain();
            if(!isTerrain)
            {
                Divide();
            }
        }

        private void Divide()
        {
            float halfHeight = mapConfig.terrainMaxHeigh / 2;
            float halfSize = bounds.size.x / 2;
            float offsetCenter = halfSize / 2;
            Vector3 childSize = new Vector3(halfSize, mapConfig.terrainMaxHeigh, halfSize);

            LeftAndTop = new Node(new Bounds(new Vector3(bounds.center.x - offsetCenter, halfHeight, bounds.center.z + offsetCenter), childSize), true);
            RightAndTop = new Node(new Bounds(new Vector3(bounds.center.x + offsetCenter, halfHeight, bounds.center.z + offsetCenter), childSize), true);
            LeftAndBottom = new Node(new Bounds(new Vector3(bounds.center.x - offsetCenter, halfHeight, bounds.center.z - offsetCenter), childSize), true);
            RightAndBottom = new Node(new Bounds(new Vector3(bounds.center.x + offsetCenter, halfHeight, bounds.center.z - offsetCenter), childSize), true);
        }

        private bool CheckTerrain()
        {
            bool _isTerrain = bounds.size.x <= mapConfig.terrainSize && bounds.size.z <= mapConfig.terrainSize;
            if(_isTerrain)
            {
                terrainCoord.x = (int)(bounds.center.x / mapConfig.terrainSize);
                terrainCoord.y = (int)(bounds.center.z / mapConfig.terrainSize);
            }
            return _isTerrain;
        }

        public bool active;
        public void CheckVisual()
        {
            bool isVisual = ClientMapManager.Instance.CheckVisual(bounds);

            if (active && !isVisual) Disable();
            else if(isVisual)
            {
                if (isTerrain)
                {
                    ClientMapManager.Instance.EnableTerrain(terrainCoord);
                }
                else
                {
                    LeftAndTop?.CheckVisual();
                    RightAndTop?.CheckVisual();
                    LeftAndBottom?.CheckVisual();
                    RightAndBottom?.CheckVisual();
                }
            }

            active = isVisual;
        }

        public void Disable()
        {
            if (isTerrain) ClientMapManager.Instance.DisableTerrain(terrainCoord);

            LeftAndTop?.Disable();
            RightAndTop?.Disable();
            LeftAndBottom?.Disable();
            RightAndBottom?.Disable();
        }

#if UNITY_EDITOR
        public void Draw()
        {
            Gizmos.color = isTerrain ? Color.green : Color.black;
            Gizmos.DrawWireCube(bounds.center, bounds.size * 0.98f);
            Gizmos.color = Color.black;
            LeftAndTop?.Draw();
            RightAndTop?.Draw();
            LeftAndBottom?.Draw();
            RightAndBottom?.Draw();
        }
#endif
    }

    public Node rootNode;

    public QuadTree(MapConfig mapConfig)
    {
        QuadTree.mapConfig = mapConfig;
        rootNode = new Node(new Bounds(new Vector3(0, mapConfig.terrainMaxHeigh / 2, 0), 
            new Vector3(mapConfig.quadTreeSize, mapConfig.terrainMaxHeigh, mapConfig.quadTreeSize)), true);
    }

    public void CheckVisual()
    {
        rootNode?.CheckVisual();
    }


#if UNITY_EDITOR
    public void Draw()
    {
        rootNode?.Draw();
    }
#endif
}
