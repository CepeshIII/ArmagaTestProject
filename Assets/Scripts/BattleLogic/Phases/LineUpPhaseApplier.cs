using Zenject;


/// <summary>
/// Battle phase applier for the "LiningUp" phase.
/// Overrides certain strategies to disable combat and targeting while lining up units.
/// </summary>
public sealed class LineUpPhaseApplier : BasePhaseApplier
{
    /// <summary>
    /// Constructs the lineup phase applier with the provided Zenject container.
    /// </summary>
    /// <param name="container">The DI container used to instantiate and inject strategies.</param>
    [Inject]
    public LineUpPhaseApplier(DiContainer container) : base(container) { }


    protected override void BeforeStrategiesBinding(DiContainer container)
    {
        container.BindInterfacesAndSelfTo<RegistryLineupPositionProvider>().FromNew()
        .AsTransient();
    }

    /// <summary>
    /// Returns the strategies to apply for the "LiningUp" phase.
    /// Overrides combat and targeting strategies to prevent attacks while lining up.
    /// </summary>
    /// <param name="baseStrategies">The default EntityStrategySet from definition.</param>
    /// <returns>A <see cref="BattleEntityStrategySet"/> containing the modified strategies for lining up.</returns>
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

