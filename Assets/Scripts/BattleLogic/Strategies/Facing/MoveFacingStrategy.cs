using UnityEngine;

public class MoveFacingStrategy : IFacingStrategy
{
    public bool TryGetFacing(
        UnitIntent intent,
        MoveData moveData,
        BattleEntity self,
        out Vector2 facing)
    {
        if (!moveData.isMoving)
        {
            facing = default;
            return false;
        }

        facing = new Vector2(moveData.direction.x, moveData.direction.y).normalized;
        return true;
    }
}
