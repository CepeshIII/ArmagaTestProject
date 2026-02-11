using System.Collections.Generic;
using UnityEngine;

public sealed class LineupEntityRegistry
{
    private readonly Dictionary<int, Vector3> positionByEntityId = new();

    public void Register(BattleEntity entity, Vector3 position)
    {
        positionByEntityId[entity.GetInstanceID()] = position;
    }

    public bool TryGet(BattleEntity entity, out Vector3 position)
    {
        return positionByEntityId.TryGetValue(entity.GetInstanceID(), out position);
    }

    public void Clear() => positionByEntityId.Clear();
}
