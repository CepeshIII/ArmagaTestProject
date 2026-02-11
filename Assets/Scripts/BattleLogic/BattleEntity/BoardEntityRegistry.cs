using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public sealed class BoardEntityAnchor
{
    public BattleEntity Entity { get; }
    public BoardCellPosition Cell { get; }
    public Vector3 PositionInCell { get; }


    public BoardEntityAnchor(
        BattleEntity entity,
        BoardCellPosition cell,
        Vector3 positionInCell)
    {
        Entity = entity;
        Cell = cell;
        PositionInCell = positionInCell;
    }
}



public sealed class BoardEntityRegistry
{
    private readonly Dictionary<int, BoardEntityAnchor> anchorByEntityId = new();


    public void Register(BattleEntity entity, BoardCellPosition cell, Vector3 position)
    {
        var boardEntityAnchor = new BoardEntityAnchor(entity, cell, position);
        
        var id = entity.GetInstanceID();
        if (!anchorByEntityId.ContainsKey(id))
        {
            anchorByEntityId.Add(id, boardEntityAnchor);
            return;
        }
        anchorByEntityId[id] = boardEntityAnchor;
    }

    public void Unregister(BattleEntity entity)
    {
        var id = entity.GetInstanceID();
        if (!anchorByEntityId.ContainsKey(id))
        {
            return;
        }

        anchorByEntityId.Remove(id);
    }

    public IEnumerable<BoardEntityAnchor> GetByCell(BoardCellPosition cell)
    {
        foreach (var anchor in anchorByEntityId.Values)
        {
            if (anchor.Equals(cell))
            {
                yield return anchor;
            }
        }
    }

    public bool TryGetByEntityID(int entityID, out BoardEntityAnchor entityAnchor)
    {
        return anchorByEntityId.TryGetValue(entityID, out entityAnchor);
    }

    public IEnumerable<BoardEntityAnchor> All => anchorByEntityId.Values;

    public void Clear() => anchorByEntityId.Clear();
}
