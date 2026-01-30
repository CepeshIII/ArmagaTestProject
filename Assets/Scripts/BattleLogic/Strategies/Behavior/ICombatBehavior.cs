public enum IntentType
{
    Idle,
    MoveToPosition,
    MoveToTarget,
    Attack
}


public interface ICombatBehavior
{
    public UnitIntent Decide(BattleEntity self, BattleEntityContext context);
}

