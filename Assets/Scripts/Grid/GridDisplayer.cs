using UnityEngine;

public struct CachedGridBounds
{
    public Vector3 pointA;
    public Vector3 pointB;
    public Vector3 pointC;
    public Vector3 pointD;

    public CachedGridBounds(GridBounds bounds)
    {
        pointA = bounds.pointA;
        pointB = bounds.pointB;
        pointC = bounds.pointC;
        pointD = bounds.pointD;
    }

    public bool IsEqual(GridBounds bounds)
    {
        return pointA == bounds.pointA &&
                pointB == bounds.pointB &&
                pointC == bounds.pointC &&
                pointD == bounds.pointD;
    }
}


[ExecuteAlways]
[RequireComponent(typeof(GridBounds))]
public class GridDisplayer : MonoBehaviour
{
    [SerializeField] private Material gridMaterial;
    [SerializeField] private Vector2Int textureSize = new Vector2Int(16, 16);

    private IsometricGrid grid;
    private GridBounds gridBounds;
    private CachedGridBounds cachedGridBounds;
    private GridShaderController gridShaderController;



#if UNITY_EDITOR

    private void OnEnable()
    {
        if (Application.isPlaying)
        {
            return;
        }

        Debug.Log("OnEnable");

        gridBounds = GetComponent<GridBounds>();
        grid = new IsometricGrid();

        grid.BuildFromBounds(gridBounds);
        cachedGridBounds = new CachedGridBounds(gridBounds);

        gridShaderController = new GridShaderController();
        gridShaderController.SetMaterial(gridMaterial);
        gridShaderController.CreateAndSetMaskTexture(new Vector2Int(16, 16));
        gridShaderController.SetGridOffset(grid.GridOffset);


        DrawGrid();
    }


    private void Update()
    {
        if (Application.isPlaying)
        {
            return;
        }


        if (gridBounds != null)
        {
            if (!cachedGridBounds.IsEqual(gridBounds) || !gridShaderController.HasTexture)
            {
                cachedGridBounds = new CachedGridBounds(gridBounds);
                grid.BuildFromBounds(gridBounds);
                gridShaderController.SetGridOffset(grid.GridOffset);

                DrawGrid();
            }
        }

    }

#endif


    private void DrawGrid()
    {
        if (grid == null || gridShaderController == null) return;
        gridShaderController.ClearMask();

        for (int y = 0; y < grid.GridSize.y; y++)
        {
            for (int x = 0; x < grid.GridSize.x; x++) 
            {
                var cellIndex = new Vector2Int(x, y);
                gridShaderController.SetMaskPixel(cellIndex, true);
            }

        }

        gridShaderController.ApplyMask();

    }
}
