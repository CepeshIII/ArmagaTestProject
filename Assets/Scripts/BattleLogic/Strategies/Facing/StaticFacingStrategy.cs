using UnityEngine;

public class StaticFacingStrategy : IFacingStrategy
{
    private readonly Vector2 facing = new Vector2(1, -1).normalized;


    public StaticFacingStrategy()
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
