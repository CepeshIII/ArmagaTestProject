using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainLineupProvider : ILineupPositionProvider
{
    private int countInLine = 10;
    private Dictionary<int, Vector2> cachedPositions = new();
    private GameBoard gameBoard;
    private ICoordinateConverter coordinateConverter;



    [Inject]
    public MainLineupProvider(GameBoard gameBoard, ICoordinateConverter coordinateConverter)
    {
        this.gameBoard = gameBoard;
        this.coordinateConverter = coordinateConverter;
    }


    public void CalculatePositionsForPlayerEntities(List<BattleEntity> entities)
    {
        if (entities.Count == 0) return;

        var gridSize = gameBoard.Grid.GridSize;

        // Define the boundaries
        var minGrid = gameBoard.Grid.IndexCoordsToGridPosition(new Vector2Int(0, gridSize.y - 2));
        var maxGrid = gameBoard.Grid.IndexCoordsToGridPosition(new Vector2Int(gridSize.x, gridSize.y));

        foreach(var (key, value) in CalculatePosition(entities, minGrid, maxGrid))
        {
            cachedPositions.Add(key, value);
        }
    }

    
    public void CalculatePositionsForEnemyEntities(List<BattleEntity> entities)
    {
        if (entities.Count == 0) return;

        var gridSize = gameBoard.Grid.GridSize;

        // Define the boundaries
        var minGrid = gameBoard.Grid.IndexCoordsToGridPosition(new Vector2Int(0, gridSize.y - 1));
        var maxGrid = gameBoard.Grid.IndexCoordsToGridPosition(new Vector2Int(gridSize.x, gridSize.y + 1));

        foreach (var (key, value) in CalculatePosition(entities, minGrid, maxGrid))
        {
            cachedPositions.Add(key, value);
        }
    }


    public Vector3 GetPosition(BattleEntity entity)
    {
        if (!cachedPositions.TryGetValue(entity.GetInstanceID(), out Vector2 position))
        {
            Debug.LogWarning($"Position for entity {entity.name} not found in cached positions.");
            return entity.transform.position;
        }

        return position;
    }


    public void Clear()
    {
        cachedPositions.Clear();
    }


    private IEnumerable<KeyValuePair<int, Vector2>> CalculatePosition(
        List<BattleEntity> entities, Vector2Int minGrid, Vector2Int maxGrid)
    {

        var centerGridPosition = (Vector2)(minGrid + maxGrid) / 2f;
        var totalUnits = entities.Count;
        var totalLines = Mathf.CeilToInt((float)totalUnits / countInLine);

        // Calculate spacing based on the battle zone size
        var spacing = new Vector2(
            (maxGrid.x - minGrid.x) / (float)countInLine,
            (maxGrid.y - minGrid.y) / (float)Mathf.Max(1, totalLines)
        );

        int unitIndex = 0;
        for (int row = 0; row < totalLines; row++)
        {
            // Determine how many units are in this specific row
            int unitsInThisRow = Mathf.Min(countInLine, totalUnits - unitIndex);

            // Calculate Y: Center the stack of rows vertically around the center point
            // (row - (totalLines - 1) / 2.0f) centers the group of rows
            float y = centerGridPosition.y - (row - (totalLines - 1) / 2.0f) * spacing.y;

            for (int col = 0; col < unitsInThisRow; col++)
            {
                // Calculate X: Center the units within this specific row
                // (col - (unitsInThisRow - 1) / 2.0f) ensures even/odd counts both center perfectly
                float x = centerGridPosition.x + (col - (unitsInThisRow - 1) / 2.0f) * spacing.x;

                Vector2 gridPos = new Vector2(x, y);
                yield return KeyValuePair.Create(entities[unitIndex].GetInstanceID(), (Vector2)gameBoard.Grid.GridPositionToWorld(gridPos));

                unitIndex++;
            }
        }
    }
}
