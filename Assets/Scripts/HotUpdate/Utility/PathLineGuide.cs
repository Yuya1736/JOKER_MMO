using JKFrame;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PathLineGuide : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private static PathLineGuide instance;
    public static PathLineGuide Instance 
    {  
        get
        {
            if (instance == null)
            {
                instance = ResSystem.InstantiateGameObject<PathLineGuide>("PathLineGuide");
            }
            return instance; 
        } 
    }

    [Header("指引线设置")]
    public float yOffset = 0.2f;      // 抬高距离，防止和地面 Z-Fighting 闪烁
    public float scrollSpeed = -2.0f; // 纹理滚动速度（负数通常是向前流动）

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

        // 确保纹理模式是 Tile，否则 UV 滚动会显得拉伸变形
        lineRenderer.textureMode = LineTextureMode.Tile;

        // 初始状态隐藏
        lineRenderer.positionCount = 0;
    }

    /// <summary>
    /// 传入 NavMeshPath.corners 来绘制路线
    /// </summary>
    public void DrawPath(Vector3[] corners)
    {
        HidePath();
        if (corners == null || corners.Length < 2)
        {
            HidePath();
            return;
        }

        lineRenderer.positionCount = corners.Length;

        // 遍历所有拐点，并稍微抬高 Y 轴
        for (int i = 0; i < corners.Length; i++)
        {
            Vector3 safePosition = corners[i];
            safePosition.y += yOffset; // 抬高，避开物理地面
            lineRenderer.SetPosition(i, safePosition);
        }
    }

    public void HidePath()
    {
        lineRenderer.positionCount = 0;
    }

    void Update()
    {
        // 如果线段正在显示，并且材质存在，则在 Update 中滚动 UV 制造“流动感”
        if (lineRenderer.positionCount > 0 && lineRenderer.material != null)
        {
            // 修改材质的 mainTextureOffset 来实现流动
            float offset = Time.time * scrollSpeed;
            lineRenderer.material.mainTextureOffset = new Vector2(offset, 0);
        }
    }
}