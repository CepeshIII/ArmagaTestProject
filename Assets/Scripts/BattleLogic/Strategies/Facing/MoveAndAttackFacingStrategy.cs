using UnityEngine;

public class MoveAndAttackFacingStrategy : IFacingStrategy
{
    private AttackFacingStrategy attackFacingStrategy;
    private MoveFacingStrategy moveFacingStrategy;


    public MoveAndAttackFacingStrategy()
    {
        attackFacingStrategy = new AttackFacingStrategy();
        moveFacingStrategy = new MoveFacingStrategy();
    }


    public bool TryGetFacing(UnitIntent intent, MoveData moveData, BattleEntity self, out Vector2 facing)
    {
        if (intent.Type != IntentType.Attack || intent.Target == null)
        {
            return moveFacingStrategy.TryGetFacing(intent, moveData, self, out facing);
        }
        else
        {
            return attackFacingStrategy.TryGetFacing(intent, moveData, self, out facing);
        }
    }
}
