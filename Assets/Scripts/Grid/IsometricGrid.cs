using System;
using UnityEngine;

public interface ILinearGrid
{
    public Vector2 CellSize { get; }
    public Vector2Int GridOffset { get; }
    public Vector2Int GridSize { get; }

    public void BuildGrid();

    public Vector2Int GridPositionToIndexCoords(Vector2Int gridPos);
    public Vector3 GridPositionToWorld(Vector2 gridPos);

    public int IndexCoordsToArrayIndex(Vector2Int indexCoords);

    public Vector2Int IndexCoordsToGridPosition(Vector2Int indexCoords);

    public Vector3 IndexCoordsToWorldCenter(Vector2Int indexCoords);
    public Vector3 IndexCoordsToWorldCorner(Vector2Int indexCoords);

    public bool IsInsideGridIndex(Vector2Int indexCoords);

    public Vector2Int WorldToGridPosition(Vector2 isoWorldPos);
    public Vector2Int WorldToIndexCoords(Vector2 isoWorldPos);
}


[RequireComponent(typeof(GridBounds))]
public class IsometricGrid : MonoBehaviour, ILinearGrid
{
    public struct Cell
    {
        public Vector2Int gridPosition;     // row/col
        public Vector2 centerRectPosition;  // center in rectangular coords
        public Vector2 centerIsoPosition;   // center in isometric coords
        public bool isOccupied;
    }


    [SerializeField] private Vector2 cellSize;
    [SerializeField] private GridBounds gridBounds;

    private Vector2Int gridOriginOffset;
    private Vector2Int gridSize;

    public Vector2 CellSize { get => cellSize; }
    public Vector2Int GridSize { get => gridSize; }
    public Vector2Int GridOffset { get => gridOriginOffset; }



    private void Awake()
    {
        gridBounds = GetComponent<GridBounds>();
        InitGridArray();
    }


    private void Update()
    {
        InitGridArray();
    }


    public void BuildGrid()
    {
        InitGridArray();
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

        gridOriginOffset = firstCellIndex + new Vector2Int(1, -1); // skip first row and column
        gridSize = new Vector2Int(innerCols, innerRows);
    }


    public bool IsInsideGridIndex(Vector2Int indexCoords)
    {
        return !(indexCoords.x >= gridSize.x || indexCoords.y >= gridSize.y
               || indexCoords.x < 0 || indexCoords.y < 0);
    }


    /// <summary>
    /// Converts world isometric IndexCoords to grid coordinates.
    /// </summary>
    /// <param name="isoWorldPos">World IndexCoords in isometric space.</param>
    public Vector2Int WorldToGridPosition(Vector2 isoWorldPos)
    {
        var rectCoords = IsoMath.ReverseIsoProject(isoWorldPos);
        return new Vector2Int(
            Mathf.FloorToInt(rectCoords.x / cellSize.x),
            Mathf.FloorToInt(rectCoords.y / cellSize.y)
        );
    }


    /// <summary>
    /// Converts world isometric IndexCoords to grid index coordinates.
    /// </summary>
    /// <param name="isoWorldPos">World IndexCoords in isometric space.</param>
    public Vector2Int WorldToIndexCoords(Vector2 isoWorldPos)
    {
        var gridPos = WorldToGridPosition(isoWorldPos);
        return GridPositionToIndexCoords(gridPos);
    }


    /// <summary>
    /// Converts grid IndexCoords to array index.
    /// </summary>
    public int IndexCoordsToArrayIndex(Vector2Int indexCoords)
    {
        return indexCoords.y * gridSize.x + indexCoords.x;
    }


    /// <summary>
    /// Converts grid IndexCoords to index coordinates (not array index).
    /// </summary>
    public Vector2Int GridPositionToIndexCoords(Vector2Int gridPos)
    {
        var localCoords = gridPos - gridOriginOffset;
        localCoords.y = -localCoords.y;
        return localCoords;
    }


    /// <summary>
    /// Converts index coordinates back to grid IndexCoords.
    /// </summary>
    public Vector2Int IndexCoordsToGridPosition(Vector2Int indexCoords)
    {
        return new Vector2Int(indexCoords.x, -indexCoords.y) + gridOriginOffset;
    }



    /// <summary>
    /// Converts grid IndexCoords to isometric world IndexCoords.
    /// </summary>
    public Vector3 GridPositionToWorld(Vector2 gridPos)
    {
        return IsoMath.IsoProject(gridPos * cellSize);
    }


    /// <summary>
    /// Converts index coordinates to isometric world IndexCoords (cell corner).
    /// </summary>
    public Vector3 IndexCoordsToWorldCorner(Vector2Int indexCoords)
    {
        var gridPos = IndexCoordsToGridPosition(indexCoords);
        return GridPositionToWorld(gridPos);
    }


    /// <summary>
    /// Converts index coordinates to isometric world IndexCoords (cell center).
    /// </summary>
    public Vector3 IndexCoordsToWorldCenter(Vector2Int indexCoords)
    {
        var gridPos = IndexCoordsToGridPosition(indexCoords);
        return GridPositionToWorld(gridPos + new Vector2(0.5f, 0.5f));
    }
}
