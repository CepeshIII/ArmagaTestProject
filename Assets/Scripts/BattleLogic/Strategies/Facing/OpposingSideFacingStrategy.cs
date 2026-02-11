using UnityEngine;

public class OpposingSideFacingStrategy : IFacingStrategy
{
    public bool TryGetFacing(
        UnitIntent intent,
        MoveData moveData,
        BattleEntity self,
        out Vector2 facing)
    {
        switch(self.Team)
        {
            case Team.Player:
                facing =  new Vector2(1, -1).normalized;
                break;
            case Team.Enemy:
                facing = new Vector2(-1, 1).normalized;
                break;
            default:
                facing = new Vector2(1, -1).normalized;
                break;
        }

        return true;
    }
}

