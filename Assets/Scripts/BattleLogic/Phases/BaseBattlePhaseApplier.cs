using Zenject;


/// <summary>
/// Base class for all battle phase appliers.
/// Handles creating a sub-container for dependency injection and applying the entity strategies.
/// </summary>
public abstract class BaseBattlePhaseApplier : IBattlePhaseApplier
{
    /// <summary>
    /// Zenject container used to resolve and inject dependencies into entities.
    /// </summary>
    protected readonly DiContainer container;

    /// <summary>
    /// Initializes the base applier with the DI container.
    /// </summary>
    /// <param name="container">The Zenject DI container.</param>
    protected BaseBattlePhaseApplier(DiContainer container)
    {
        this.container = container;
    }


    /// <summary>
    /// Applies the strategies for this phase to the specified entity.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <param name="baseStrategies">The default EntityStrategySet from definition.</param>
    public void Apply(BattleEntity entity, BattleEntityStrategySet baseStrategies)
    {
        var strategies = GetPhaseStrategies(baseStrategies);
        ApplyStrategies(entity, strategies);
    }


    /// <summary>
    /// Binds the strategies in a sub-container and applies them to the entity via <see cref="BattleEntityComposer"/>.
    /// </summary>
    /// <param name="entity">The target entity.</param>
    /// <param name="strategies">The strategies container to apply.</param>
    protected void ApplyStrategies(BattleEntity entity, BattleEntityStrategySet strategies)
    {
        var sub = container.CreateSubContainer();

        // Bind the entity instance to the sub-container so composer can inject it
        sub.Bind<BattleEntity>().FromInstance(entity).AsSingle();

        // Bind all strategies and inject them into the composer
        BattleEntityStrategyInstaller.Bind(sub, strategies);

        // Apply all injected strategies to the entity
        sub.Instantiate<BattleEntityComposer>().Apply();
    }


    /// <summary>
    /// Must be implemented by derived classes to provide the strategies specific to this phase.
    /// </summary>
    /// <param name="baseStrategies">The default EntityStrategySet from definition.</param>
    /// <returns>A <see cref="BattleEntityStrategySet"/> with the phase-specific strategies.</returns>
    protected abstract BattleEntityStrategySet GetPhaseStrategies(BattleEntityStrategySet baseStrategies);
}

