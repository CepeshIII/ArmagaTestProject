using System.Collections.Generic;
using UnityEngine;
using Zenject;


public sealed class BattleLineupPreparer
{
    private readonly ILineupPlacementStrategy placementStrategy;
    private readonly LineupEntityRegistry registry;
    private readonly GameBoard gameBoard;


    [Inject]
    public BattleLineupPreparer(
        ILineupPlacementStrategy placementStrategy,
        LineupEntityRegistry registry,
        GameBoard gameBoard)
    {
        this.placementStrategy = placementStrategy;
        this.registry = registry;
        this.gameBoard = gameBoard;
    }


    public void Prepare(
        IReadOnlyList<BattleEntity> players,
        IReadOnlyList<BattleEntity> enemies)
    {
        registry.Clear();

        PrepareTeam(players, GetPlayerArea());
        PrepareTeam(enemies, GetEnemyArea());
    }


    private void PrepareTeam(
        IReadOnlyList<BattleEntity> entities,
        LineupArea area)
    {
        if (entities.Count == 0)
            return;

        var positions = placementStrategy.GetPositions(entities, area);

        for (int i = 0; i < entities.Count; i++)
        {
            registry.Register(entities[i], positions[i]);
        }
    }


    private LineupArea GetPlayerArea()
    {
        var gridSize = gameBoard.Grid.GridSize;

        return new LineupArea(
            min: new Vector2Int(0, gridSize.y - 2),
            max: new Vector2Int(gridSize.x, gridSize.y)
        );
    }


    private LineupArea GetEnemyArea()
    {
        var gridSize = gameBoard.Grid.GridSize;

        return new LineupArea(
            min: new Vector2Int(0, gridSize.y - 1),
            max: new Vector2Int(gridSize.x, gridSize.y + 1)
        );
    }
}
