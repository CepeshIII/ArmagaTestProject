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

        maskController = new GridShaderController();
        maskController.SetMaterial(boardMaterial);
        maskController.CreateAndSetMaskTexture(textureSize);
        maskController.SetGridOffset(grid.GridOffset);

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
                maskController.SetGridOffset(grid.GridOffset);
                DrawGrid();
            }
        }
    }


    private void DrawGrid()
    {
        if (grid == null || maskController == null) return;
        maskController.ClearMask();

        for (int x = 0; x < grid.GridSize.x; x++) 
        { 
            for(int y = 0; y < grid.GridSize.y; y++)
            {
                maskController.SetMaskPixel(new Vector2Int(x, y), true);
            }
        
        }

        maskController.ApplyMask();

    }*/
}
