using UnityEngine;
using Zenject;

public class LineupToBoardCellProvider : ILineupPositionProvider
{
    private readonly BoardEntityRegistry boardEntityRegistry;


    [Inject]
    public LineupToBoardCellProvider(BoardEntityRegistry boardEntityRegistry)
    {
        this.boardEntityRegistry = boardEntityRegistry;
    }


    public Vector3 GetPosition(BattleEntity entity)
    {
        if(boardEntityRegistry.TryGetByEntityID(entity.GetInstanceID(), out var entityAnchor))
        {
            return entityAnchor.PositionInCell;
        }

        return entity.transform.position;
    }

}

