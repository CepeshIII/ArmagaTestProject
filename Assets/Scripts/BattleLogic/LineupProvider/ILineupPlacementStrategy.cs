using System.Collections.Generic;
using UnityEngine;

public interface ILineupPlacementStrategy
{
    Vector3[] GetPositions(
        IReadOnlyList<BattleEntity> entities,
        LineupArea area
    );
}
