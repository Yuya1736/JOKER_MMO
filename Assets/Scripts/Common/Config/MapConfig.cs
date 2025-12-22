using JKFrame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapConfig", menuName = "GenerateConfig/MapConfig")]
public class MapConfig : ConfigBase
{
    // 300 * 4 * 4 * 4
    public int quadTreeSize = 19200;
    public Vector2Int mapSize = new Vector2Int(12000, 12000);
    public Vector2Int terrainCoordOffset;
    public float terrainSize = 300;
    public float terrainMaxHeigh = 200;

    private void OnValidate()
    {
        terrainCoordOffset = new Vector2Int((int)(mapSize.x / terrainSize / 2), (int)(mapSize.y / terrainSize / 2));
    }
}
