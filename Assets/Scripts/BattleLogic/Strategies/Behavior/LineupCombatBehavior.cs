using Zenject;


public sealed class LineupCombatBehavior : ICombatBehavior
{
    private readonly ILineupPositionProvider lineupProvider;



    [Inject]
    public LineupCombatBehavior(ILineupPositionProvider lineupProvider)
    {
        this.lineupProvider = lineupProvider;
    }


    public UnitIntent Decide(BattleEntity self, BattleEntityContext context)
    {
        var pos = lineupProvider.GetPosition(self);
        return UnitIntent.MoveToPosition(pos);
    }
}
