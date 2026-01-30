using System.Collections.Generic;
using UnityEngine;
using Zenject;



/// <summary>
/// Controls the application of different battle phases to a collection of <see cref="BattleEntity"/>s.
/// Each phase defines which strategies (movement, combat, targeting) the entities should use.
/// </summary>
public sealed class BattlePhaseController
{
    /// <summary>
    /// Handles applying the "LiningUp" phase strategies to entities.
    /// </summary>
    private readonly IBattlePhaseApplier lineUpApplier;

    /// <summary>
    /// Handles applying the "Combat" phase strategies to entities.
    /// </summary>
    private readonly IBattlePhaseApplier combatApplier;


    /// <summary>
    /// Creates a new instance of <see cref="BattlePhaseController"/> and injects the necessary phase appliers.
    /// </summary>
    /// <param name="lineUpApplier">Applier for the LiningUp phase.</param>
    /// <param name="combatApplier">Applier for the Combat phase.</param>
    [Inject]
    public BattlePhaseController(
        LineUpPhaseApplier lineUpApplier,
        BattlePhaseApplier combatApplier)
    {
        this.lineUpApplier = lineUpApplier;
        this.combatApplier = combatApplier;
    }


    /// <summary>
    /// Applies the specified <paramref name="phase"/> to all <paramref name="entities"/>.
    /// Each entity's strategies are updated according to the phase.
    /// </summary>
    /// <param name="phase">The battle phase to apply (e.g., LiningUp, Combat).</param>
    /// <param name="entities">The list of entities to update.</param>
    public void ApplyPhase(
        BattlePhase phase,
        IReadOnlyList<BattleEntity> entities)
    {
        foreach (var entity in entities)
        {
            var baseStrategies = entity.BaseStrategies;

            switch (phase)
            {
                case BattlePhase.LiningUp:
                    lineUpApplier.Apply(entity, baseStrategies);
                    break;

                case BattlePhase.Combat:
                    combatApplier.Apply(entity, baseStrategies);
                    break;
            }
        }
    }
}

