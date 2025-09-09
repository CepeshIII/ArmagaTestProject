using UnityEngine;
using UnityEngine.Tilemaps;


[RequireComponent(typeof(GridBounds))]
public class IsometricGrid : MonoBehaviour
{
    private struct Cell
    {
        public Vector2Int gridPosition;
        public Vector2 centerRectPosition;
        public Vector2 centerIsoPosition;
        public bool isOccupied;
    }

    private struct MouseClickInfo
    {
        public Vector2Int cellIndex;

        public Vector2 rectPosition; // position in rectangular space
        public Vector2 isoPosition; // position in isometric space
    }


    [SerializeField] private Vector2 cellSize;
    [SerializeField] private GridBounds gridBounds;

    // For debugging with Tilemap
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase TileBase;

    private Vector2Int indexOffset;

    private Cell[] cells;
    private MouseClickInfo mouseClickInfo;

    public Vector2 CellSize { get => cellSize; set => cellSize = value; }


    private void Awake()
    {
        gridBounds = GetComponent<GridBounds>();
    }


    private void Start()
    {
        InitGridArray();
    }


    private void Update()
    {
        InitGridArray();
        if (Input.GetMouseButtonDown(0))
        {
            var mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = 0f;

            var mouseIsoPosition = mouseWorldPosition; // We see game in isometric space,
                                                       // so our world position is isometric position

            // Transform isometric position to rectangular position
            var mouseRectPosition = ReverseIsoProject(mouseIsoPosition);

            // Find the cell index in rectangular space
            var cellIndex = new Vector2Int(
                Mathf.FloorToInt(mouseRectPosition.x / cellSize.x),
                Mathf.FloorToInt(mouseRectPosition.y / cellSize.y)
            );


            // Compute world center of that cell
            var cellCenterRect = new Vector2(
                (cellIndex.x + 0.5f) * cellSize.x,
                (cellIndex.y + 0.5f) * cellSize.y
            );


            mouseClickInfo.rectPosition = mouseRectPosition;
            mouseClickInfo.isoPosition = mouseIsoPosition;
            mouseClickInfo.cellIndex = cellIndex - indexOffset; // move index to origin

            // Debug output
            Debug.Log($"MousePosition: {mouseWorldPosition}, mouseRectPosition: {mouseRectPosition}");
            Debug.Log($"globalCellIndex: {cellIndex}, moved to origin cellIndex: {mouseClickInfo.cellIndex}");

            tilemap.SetTile((Vector3Int)(cellIndex), TileBase);
        }
    }


    private void InitGridArray()
    {
        // Find the indices of the outermost top-left and bottom-right cells
        var firstCellIndex = new Vector2Int(
            Mathf.FloorToInt(gridBounds.pointA.x / cellSize.x),
            Mathf.FloorToInt(gridBounds.pointA.y / cellSize.y)
        );
        var lastCellIndex = new Vector2Int(
            Mathf.FloorToInt(gridBounds.pointC.x / cellSize.x),
            Mathf.FloorToInt(gridBounds.pointC.y / cellSize.y)
        );

        // Total cells including the border
        var totalCols = Mathf.Abs(lastCellIndex.x - firstCellIndex.x) + 1;
        var totalRows = Mathf.Abs(lastCellIndex.y - firstCellIndex.y) + 1;

        // Exclude the border cells
        var innerCols = Mathf.Max(0, totalCols - 2);
        var innerRows = Mathf.Max(0, totalRows - 2);

        cells = new Cell[innerCols * innerRows];
        indexOffset = firstCellIndex + new Vector2Int(1, -1); // skip first row and column

        // Calculate the cell center in rectangular (grid) space.
        // Store both the rectangular and isometric positions.
        // This lets us keep grid indexing simple while still being able
        // to place objects correctly in isometric world space.
        int index = 0;
        for (int row = 1; row < totalRows - 1; row++) // skip first and last row
        {
            for (int col = 1; col < totalCols - 1; col++) // skip first and last column
            {
                // Calculate the cell center in rect coordinates
                var center = new Vector2(
                    (firstCellIndex.x + col + 0.5f) * cellSize.x,
                    (firstCellIndex.y - row + 0.5f) * cellSize.y // minus row to go down
                );

                // Store both rect position and isometric projection
                cells[index].centerRectPosition = center;
                cells[index].centerIsoPosition = IsoProject(center);
                cells[index].isOccupied = false;
                cells[index].gridPosition = new Vector2Int(row, col);

                index++;
            }
        }
    }

    /// <summary>
    /// Transform a 2D position to isometric projection
    /// </summary>
    /// <param name="position">world position</param>
    public static Vector3 IsoProject(Vector2 position)
    {
        return new Vector3(
            (position.x - position.y),
            (position.x + position.y) * 0.5f
        );
    }


    /// <summary>
    /// Transform isometric position back to 2D world position
    /// </summary>
    /// <param name="position">isometric position</param>
    public static Vector3 ReverseIsoProject(Vector2 position)
    {
        return new Vector2(
            (position.x + position.y / 0.5f) * 0.5f,
            (position.y / 0.5f - position.x) * 0.5f
        );
    }

    /// <summary>
    /// Return the cell index in grid coordinates from a world position
    /// </summary>
    /// <param name="worldPosition"> world position</param>
    /// <returns></returns>
    public Vector2Int WorldToCellIndex(Vector2 worldPosition)
    {
        var deformedPosition = ReverseIsoProject(worldPosition);

        var cellIndex = new Vector2Int(
            Mathf.FloorToInt(deformedPosition.x / cellSize.x),
            Mathf.FloorToInt(deformedPosition.y / cellSize.y)
        );

        cellIndex.y = -cellIndex.y; // y axis is inverted

        return cellIndex;
    }


    private void DrawCellRectProjection(Vector2 position, Vector2 cellSize, Color color)
    {
        var halfCellSize = (cellSize) / 2f;

        // Draw Normal Projection
        var leftUpCorner = (position + new Vector2(-halfCellSize.x, halfCellSize.y));
        var rightDownCorner = (position + new Vector2(halfCellSize.x, -halfCellSize.y));
        var leftDownCorner = (position + new Vector2(-halfCellSize.x, -halfCellSize.y));
        var rightUpCorner = (position + new Vector2(halfCellSize.x, halfCellSize.y));

        DrawRect(leftUpCorner, rightUpCorner, rightDownCorner, leftDownCorner, color);
    }


    private static void DrawCellIsometricProjection(Vector2 position, Vector2 cellSize, Color color)
    {
        var halfCellSize = (cellSize) / 2f;

        // Draw Isometric Projection
        var leftUpCorner = IsoProject(position + new Vector2(-halfCellSize.x, halfCellSize.y));
        var rightUpCorner = IsoProject(position + new Vector2(halfCellSize.x, halfCellSize.y));
        var rightDownCorner = IsoProject(position + new Vector2(halfCellSize.x, -halfCellSize.y));
        var leftDownCorner = IsoProject(position + new Vector2(-halfCellSize.x, -halfCellSize.y));

        DrawRect(leftUpCorner, rightUpCorner, rightDownCorner, leftDownCorner, color);
    }


    private static void DrawRect(Vector3 a, Vector3 b, Vector3 c, Vector3 d, Color color)
    {
        Gizmos.color = color;
        Gizmos.DrawLine(a, b);
        Gizmos.DrawLine(b, c);
        Gizmos.DrawLine(c, d);
        Gizmos.DrawLine(d, a);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(mouseClickInfo.rectPosition, 0.1f);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(mouseClickInfo.isoPosition, 0.1f);

        if (cells == null || cells.Length == 0)
            return;

        foreach (var cell in cells)
        {
            DrawCellRectProjection(cell.centerRectPosition, cellSize, Color.black);
            DrawCellIsometricProjection(cell.centerRectPosition, cellSize, Color.blue);
        }
    }
}
