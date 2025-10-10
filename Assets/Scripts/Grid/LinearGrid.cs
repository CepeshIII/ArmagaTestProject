using UnityEngine;


public class LinearGrid : ILinearGrid
{
    private Vector2 cellSize;

    private Vector2Int gridOriginOffset;
    private Vector2Int gridSize;

    private ICoordinateConverter coordConverter;

    public Vector2 CellSize { get => cellSize; }
    public Vector2Int GridSize { get => gridSize; }
    public Vector2Int GridOffset { get => gridOriginOffset; }



    public LinearGrid(Vector2 cellSize, ICoordinateConverter coordConverter)
    {
        this.cellSize = cellSize;
        this.coordConverter = coordConverter;
    }


    public void BuildGrid(GridBounds bounds)
    {
        // Find the indices of the outermost top-left and bottom-right cells
        var firstCellIndex = new Vector2Int(
            Mathf.FloorToInt(bounds.pointA.x / cellSize.x),
            Mathf.FloorToInt(bounds.pointA.y / cellSize.y)
        );
        var lastCellIndex = new Vector2Int(
            Mathf.FloorToInt(bounds.pointC.x / cellSize.x),
            Mathf.FloorToInt(bounds.pointC.y / cellSize.y)
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


    /// <summary>
    /// Converts grid position to index coordinates (not array index).
    /// </summary>
    public Vector2Int GridPositionToIndexCoords(Vector2Int gridPos)
    {
        var localCoords = gridPos - gridOriginOffset;
        localCoords.y = -localCoords.y;
        return localCoords;
    }


    public bool IsInsideGridIndex(Vector2Int indexCoords)
    {
        return !(indexCoords.x >= gridSize.x || indexCoords.y >= gridSize.y
               || indexCoords.x < 0 || indexCoords.y < 0);
    }


    /// <summary>
    /// Converts world position to grid coordinates.
    /// </summary>
    public Vector2Int WorldToGridPosition(Vector2 worldPos)
    {
        var position = coordConverter.ConvertIn(worldPos);
        return new Vector2Int(
            Mathf.FloorToInt(position.x / cellSize.x),
            Mathf.FloorToInt(position.y / cellSize.y)
        );
    }


    /// <summary>
    /// Converts world position to grid index coordinates.
    /// </summary>
    public Vector2Int WorldToIndexCoords(Vector2 worldPos)
    {
        var gridPos = WorldToGridPosition(worldPos);
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
    /// Converts index coordinates back to grid position.
    /// </summary>
    public Vector2Int IndexCoordsToGridPosition(Vector2Int indexCoords)
    {
        return new Vector2Int(indexCoords.x, -indexCoords.y) + gridOriginOffset;
    }


    /// <summary>
    /// Converts grid position to world position
    /// </summary>
    public Vector3 GridPositionToWorld(Vector2 gridPos)
    {
        return coordConverter.ConvertOut(gridPos * cellSize);
    }


    /// <summary>
    /// Converts index coordinates to world position (cell corner).
    /// </summary>
    public Vector3 IndexCoordsToWorldCorner(Vector2Int indexCoords)
    {
        var gridPos = IndexCoordsToGridPosition(indexCoords);
        return GridPositionToWorld(gridPos);
    }


    /// <summary>
    /// Converts index coordinates to world (cell center).
    /// </summary>
    public Vector3 IndexCoordsToWorldCenter(Vector2Int indexCoords)
    {
        var gridPos = IndexCoordsToGridPosition(indexCoords);
        return GridPositionToWorld(gridPos + new Vector2(0.5f, 0.5f));
    }
}