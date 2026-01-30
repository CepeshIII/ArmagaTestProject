using UnityEngine;

public class StaticFacingStrategy : IFacingStrategy
{
    private readonly Vector2 facing;

    public StaticFacingStrategy(Vector2 facing)
    {
        this.facing = facing.normalized;
    }

    public bool TryGetFacing(
        UnitIntent intent,
        MoveData moveData,
        BattleEntity self,
        out Vector2 outFacing)
    {
        outFacing = facing;
        return true;
    }
}
