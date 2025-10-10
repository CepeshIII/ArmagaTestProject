using UnityEngine;


[ExecuteAlways]
[RequireComponent(typeof(GridBoundsBehaviour))]
public class GridDisplayer : MonoBehaviour
{
    [SerializeField] private Material gridMaterial;
    [SerializeField] private Vector2Int textureSize = new Vector2Int(16, 16);

    private ILinearGrid grid;
    private GridBoundsBehaviour gridBounds;
    private CachedGridBounds cachedGridBounds;
    private GridShaderController gridShaderController;



#if UNITY_EDITOR

    private void OnEnable()
    {
        if (Application.isPlaying)
        {
            return;
        }

        gridBounds = GetComponent<GridBoundsBehaviour>();
        grid = new LinearGrid(Vector2.one, 
            new IsometricToWorldCoordinateConverter());
        grid.BuildGrid(gridBounds.bounds);
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
                grid.BuildGrid(gridBounds.bounds);
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
                gridShaderController.SetBorderPixel(cellIndex, true);
            }

        }

        gridShaderController.ApplyMask();

    }
}
