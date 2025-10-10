using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// Defines a strategy for placing multiple objects within an grid cell
/// in a structured row-column layout.
/// </summary>
public class RowsPlacementStrategy : ICellPlacementStrategy
{
    private readonly ILinearGrid grid;

    private readonly int maxPerRow = 5;
    private readonly int maxPerColumn = 5;
    private readonly float xOffset = 0.2f;
    private readonly float yOffset = 0.2f;


    /// <summary>
    /// Initializes a new instance of the <see cref="RowsPlacementStrategy"/> class.
    /// </summary>
    /// <param name="grid">The grid used for coordinate conversions.</param>
    /// <param name="maxPerRow">Maximum number of objects per row.</param>
    /// <param name="maxPerColumn">Maximum number of rows (columns visually).</param>
    /// <param name="xOffset">Horizontal spacing between objects.</param>
    /// <param name="yOffset">Vertical spacing between rows.</param>
    public RowsPlacementStrategy(ILinearGrid grid, 
        int maxPerRow = 5, int maxPerColumn = 5, 
        float xOffset = 0.2f, float yOffset = 0.2f)
    {
        this.grid = grid;

        this.maxPerRow = maxPerRow;
        this.maxPerColumn = maxPerColumn;
        this.xOffset = xOffset;
        this.yOffset = yOffset;
    }


    /// <summary>
    /// Calculates the world rowPositions for objects placed in a cell according to this strategy.
    /// </summary>
    /// <param name="cellCoords">The grid cell coordinates.</param>
    /// <param name="objectCount">The total number of objects to place.</param>
    /// <returns>An array of world rowPositions for object placement.</returns>
    public Vector3[] GetPositions(Vector2Int cellCoords, int objectCount)
    {
        var positions = new List<Vector3>();
        var gridPos = grid.IndexCoordsToGridPosition(cellCoords);
        var rowsGroups = CalculateRows(gridPos + new Vector2(0.5f, 0.5f), objectCount);

        foreach (var row in rowsGroups)
        {
            foreach (var pos in row)
            {
                positions.Add(grid.GridPositionToWorld(pos));
            }
        }

        return positions.ToArray();
    }
    

    /// <summary>
    /// Calculates the list of rows (each containing grid rowPositions) for the given object totalCount.
    /// </summary>
    private List<List<Vector3>> CalculateRows(Vector2 origin, int totalCount)
    {
        var rows = new List<List<Vector3>>();
        var offsetY = 0f;

        var rowCount = Mathf.Min(
            Mathf.CeilToInt((float)totalCount / maxPerRow),
            maxPerColumn);

        if (rowCount % 2 == 1)
        {
            var objectsInRow = Mathf.Min(totalCount, maxPerRow);
            rows.Add(CalculateSingleRow(origin, objectsInRow));

            totalCount -= objectsInRow;
            offsetY += yOffset;
            rowCount--;
        }
        else
        {
            offsetY += yOffset / 2;
        }

        while (rowCount > 0 && totalCount > 0)
        {
            var topRowCount = Mathf.Min(totalCount, maxPerRow);
            rows.Add(CalculateSingleRow(origin + new Vector2(0f, offsetY), topRowCount));
            totalCount -= topRowCount;

            var bottomRowCount = Mathf.Min(totalCount, maxPerRow);
            rows.Add(CalculateSingleRow(origin + new Vector2(0f, -offsetY), bottomRowCount));
            totalCount -= bottomRowCount;

            offsetY += yOffset;
            rowCount -= 2;
        }

        return rows;
    }


    /// <summary>
    /// Calculates the horizontal rowPositions for a single row of objects.
    /// </summary>
    private List<Vector3> CalculateSingleRow(Vector2 origin, int count)
    {
        var rowPositions = new List<Vector3>();
        var offsetX = 0f;

        if (count % 2 == 1)
        {
            rowPositions.Add(origin);
            offsetX += xOffset;
            count--;
        }
        else
        {
            offsetX += xOffset / 2;
        }

        while (count > 0)
        {
            rowPositions.Add(origin + new Vector2(offsetX, 0f));
            rowPositions.Add(origin + new Vector2(-offsetX, 0f));

            count -= 2;
            offsetX += xOffset;
        }

        return rowPositions;
    }

}
