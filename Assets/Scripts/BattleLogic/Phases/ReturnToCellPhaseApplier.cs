using Zenject;

public sealed class ReturnToCellPhaseApplier: BasePhaseApplier
{
    private readonly DiContainer _container;


    public ReturnToCellPhaseApplier(DiContainer container) : base(container)
    {
        _container = container;
    }


    protected override void BeforeStrategiesBinding(DiContainer container)
    {
        container.BindInterfacesAndSelfTo<LineupToBoardCellProvider>()
            .FromNew().AsTransient();
    }


    protected override BattleEntityStrategySet GetPhaseStrategies(BattleEntityStrategySet baseStrategies)
    {
        // Start with the base strategies from the entity definition
        var strategies = new BattleEntityStrategySet(baseStrategies);

        // Override strategies specific to the LiningUp phase
        strategies.SetCombatBehavior(StrategyType<ICombatBehavior>.From<LineupCombatBehavior>()); // Disable combat logic
        strategies.SetAttackStrategy(StrategyType<IAttackStrategy>.From<NoAttackStrategy>());     // Prevent attacks
        strategies.SetTargetFinder(StrategyType<ITargetFinder>.From<NoTargetFinder>());           // Disable targeting

        return strategies;
    }
}

