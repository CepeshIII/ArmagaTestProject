using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class GridLineupPlacementStrategy : ILineupPlacementStrategy
{
    private readonly GameBoard gameBoard;
    private readonly int countInLine;


    [Inject]
    public GridLineupPlacementStrategy(GameBoard gameBoard, int countInLine = 10)
    {
        this.gameBoard = gameBoard;
        this.countInLine = countInLine;
    }


    public Vector3[] GetPositions(
        IReadOnlyList<BattleEntity> entities,
        LineupArea area)
    {
        if (entities.Count == 0)
            return Array.Empty<Vector3>();

        var minGrid = gameBoard.Grid.IndexCoordsToGridPosition(area.MinGrid);
        var maxGrid = gameBoard.Grid.IndexCoordsToGridPosition(area.MaxGrid);

        var center = (Vector2)(minGrid + maxGrid) / 2f;

        int totalUnits = entities.Count;
        int totalLines = Mathf.CeilToInt((float)totalUnits / countInLine);

        var spacing = new Vector2(
            (maxGrid.x - minGrid.x) / (float)countInLine,
            (maxGrid.y - minGrid.y) / (float)Mathf.Max(1, totalLines)
        );

        var result = new Vector3[totalUnits];
        int index = 0;

        for (int row = 0; row < totalLines; row++)
        {
            int unitsInRow = Mathf.Min(countInLine, totalUnits - index);
            float y = center.y - (row - (totalLines - 1) / 2f) * spacing.y;

            for (int col = 0; col < unitsInRow; col++)
            {
                float x = center.x + (col - (unitsInRow - 1) / 2f) * spacing.x;
                result[index++] =
                    gameBoard.Grid.GridPositionToWorld(new Vector2(x, y));
            }
        }

        return result;
    }
}
