using UnityEngine;

public interface IFacingStrategy
{
    bool TryGetFacing(
        UnitIntent intent,
        MoveData moveData,
        BattleEntity self,
        out Vector2 facing
    );
}
