using UnityEngine;

public class AttackFacingStrategy : IFacingStrategy
{
    public bool TryGetFacing(
        UnitIntent intent,
        MoveData moveData,
        BattleEntity self,
        out Vector2 facing)
    {
        if (intent.Type != IntentType.Attack || intent.Target == null)
        {
            facing = default;
            return false;
        }

        var dir = intent.Target.transform.position - self.transform.position;
        facing = new Vector2(dir.x, dir.z).normalized;
        return true;
    }
}

