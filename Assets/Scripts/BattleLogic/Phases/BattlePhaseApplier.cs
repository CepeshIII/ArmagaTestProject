using Zenject;


/// <summary>
/// Battle phase applier for the general combat phase.
/// Applies the default strategies defined in the entity definition.
/// </summary>
public sealed class BattlePhaseApplier : BasePhaseApplier
{
    /// <summary>
    /// Constructs the combat phase applier with the provided Zenject container.
    /// </summary>
    /// <param name="container">The DI container used to instantiate and inject strategies.</param>
    [Inject]
    public BattlePhaseApplier(DiContainer container) : base(container) { }


    /// <summary>
    /// Returns the strategies to apply for the combat phase.
    /// In this case, the strategies are taken directly from the entity definition.
    /// </summary>
    /// <param name="baseStrategies">The default EntityStrategySet from definition.</param>
    /// <returns>A <see cref="BattleEntityStrategySet"/> containing the strategies for this phase.</returns>
    protected override BattleEntityStrategySet GetPhaseStrategies(BattleEntityStrategySet baseStrategies)
    {
        // Create a container with all strategies from the entity definition
        var strategies = new BattleEntityStrategySet(baseStrategies);
        return strategies;
    }
}
