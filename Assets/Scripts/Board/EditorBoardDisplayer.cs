using UnityEngine;


[ExecuteAlways]
public class EditorBoardDisplayer : MonoBehaviour
{
    [SerializeField] private Material boardMaterial;
    [SerializeField] private Vector2Int textureSize = new Vector2Int(16, 16);

    private GridShaderController gridShaderController;
    private GridBoundsBehaviour gridBounds;
    private ILinearGrid grid;

    private CachedGridBounds cachedBounds;



/*    private void Awake()
    {

        bounds  = GameObject.FindAnyObjectByType<GridBoundsBehaviour>();
        cachedBounds = new CachedGridBounds(bounds);

        grid = new IsometricGrid();
        grid.BuildFromBoundsBehaviour(bounds);

        maskController = new GridShaderController();
        maskController.SetMaterial(boardMaterial);
        maskController.CreateAndSetMaskTexture(textureSize);
        maskController.SetGridOffset(grid.GridOffset);

        DrawGrid();

        Debug.Log("EditorBoardDisplayer Start");
    }


    private void LateUpdate()
    {
        if (bounds != null && grid != null) 
        {
            if (!cachedBounds.IsEqual(bounds))
            {
                grid.BuildFromBoundsBehaviour(bounds);
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
