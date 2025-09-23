using UnityEngine;


[ExecuteAlways]
public class EditorBoardDisplayer : MonoBehaviour
{
    [SerializeField] private Material boardMaterial;
    [SerializeField] private Vector2Int textureSize = new Vector2Int(16, 16);

    private GridShaderController gridShaderController;
    private GridBounds gridBounds;
    private IsometricGrid grid;

    private CachedGridBounds cachedBounds;



/*    private void Awake()
    {

        gridBounds  = GameObject.FindAnyObjectByType<GridBounds>();
        cachedBounds = new CachedGridBounds(gridBounds);

        grid = new IsometricGrid();
        grid.BuildFromBounds(gridBounds);

        gridShaderController = new GridShaderController();
        gridShaderController.SetMaterial(boardMaterial);
        gridShaderController.CreateAndSetMaskTexture(textureSize);
        gridShaderController.SetGridOffset(grid.GridOffset);

        DrawGrid();

        Debug.Log("EditorBoardDisplayer Start");
    }


    private void LateUpdate()
    {
        if (gridBounds != null && grid != null) 
        {
            if (!cachedBounds.IsEqual(gridBounds))
            {
                grid.BuildFromBounds(gridBounds);
                gridShaderController.SetGridOffset(grid.GridOffset);
                DrawGrid();
            }
        }
    }


    private void DrawGrid()
    {
        if (grid == null || gridShaderController == null) return;
        gridShaderController.ClearMask();

        for (int x = 0; x < grid.GridSize.x; x++) 
        { 
            for(int y = 0; y < grid.GridSize.y; y++)
            {
                gridShaderController.SetMaskPixel(new Vector2Int(x, y), true);
            }
        
        }

        gridShaderController.ApplyMask();

    }*/
}
