/// <summary>
/// Interface for applying a battle phase to a <see cref="BattleEntity"/>.
/// Implementers define how a specific phase modifies an entity's strategies.
/// </summary>
public interface IBattlePhaseApplier
{
    /// <summary>
    /// Applies the phase-specific strategies to the given entity.
    /// </summary>
    /// <param name="entity">The entity to apply the strategies to.</param>
    /// <param name="definition">The entity definition containing base strategy implementations.</param>
    void Apply(BattleEntity entity, BattleEntityStrategySet baseStrategies);
}
