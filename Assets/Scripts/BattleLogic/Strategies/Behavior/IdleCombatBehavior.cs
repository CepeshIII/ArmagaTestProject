public class IdleCombatBehavior : ICombatBehavior
{
    public UnitIntent Decide(BattleEntity self, BattleEntityContext context)
    {
        return UnitIntent.Idle();
    }
}