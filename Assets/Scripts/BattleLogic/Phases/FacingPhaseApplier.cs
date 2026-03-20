using Zenject;
/// <summary>
/// Facing phase applier for facing entities before/after combat phase.
/// </summary>
public sealed class FacingPhaseApplier : BasePhaseApplier
{
    /// <summary>
    /// Constructs the combat phase applier with the provided Zenject settingsContainer.
    /// </summary>
    /// <param displayName="settingsContainer">The DI settingsContainer used to instantiate and inject strategies.</param>
    [Inject]
    public FacingPhaseApplier(DiContainer container) : base(container) { }


    /// <summary>
    /// Returns the strategies to apply for the combat phase.
    /// In this case, the strategies are taken directly from the entity definition.
    /// </summary>
    /// <param displayName="baseStrategies">The default EntityStrategySet from definition.</param>
    /// <returns>A <see cref="BattleEntityStrategySet"/> containing the strategies for this phase.</returns>
    protected override BattleEntityStrategySet GetPhaseStrategies(BattleEntityStrategySet baseStrategies)
    {
        // Create a settingsContainer with all strategies from the entity definition
        var strategies = new BattleEntityStrategySet(baseStrategies);
        strategies.SetFacingStrategy(StrategyType<IFacingStrategy>.From<OpposingSideFacingStrategy>());
        strategies.SetMovementStrategy(StrategyType<IMovementStrategy>.From<NoMovementStrategy>());
        strategies.SetCombatBehavior(StrategyType<ICombatBehavior>.From<IdleCombatBehavior>());
        strategies.SetAttackStrategy(StrategyType<IAttackStrategy>.From<NoAttackStrategy>());

        return strategies;
    }
}
