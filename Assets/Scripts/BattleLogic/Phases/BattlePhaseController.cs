using System;
using System.Collections.Generic;
using Zenject;



/// <summary>
/// Controls the application of different battle phases to a collection of <see cref="BattleEntity"/>s.
/// Each phase defines which strategies (movement, combat, targeting) the entities should use.
/// </summary>
public sealed class BattlePhaseController: IInitializable, IDisposable
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
    /// Handles applying the "Facing" phase strategies to entities.
    /// </summary>
    private readonly IBattlePhaseApplier facingApplier;

    /// <summary>
    /// Handles applying the "Return To Cell Phase" phase strategies to entities.
    /// </summary>
    private readonly IBattlePhaseApplier returnToCellPhaseApplier;

    /// <summary>
    /// Registry containing all archetypes of battle entities. Provides default contexts and strategies
    /// for resetting or initializing entities based on their archetype ID.
    /// </summary>
    private readonly BattleEntityArchetypeRegistry archetypeRegistry;

    private readonly UnitsManager unitsManager;

    private BattlePhase currentPhase = BattlePhase.Facing;


    /// <summary>
    /// Creates a new instance of <see cref="BattlePhaseController"/> and injects the necessary phase appliers.
    /// </summary>
    /// <param name="lineUpApplier">Applier for the LiningUp phase.</param>
    /// <param name="combatApplier">Applier for the Combat phase.</param>
    [Inject]
    public BattlePhaseController(
        BattleEntityArchetypeRegistry archetypeRegistry,
        UnitsManager unitsManager,
        LineUpPhaseApplier lineUpApplier,
        BattlePhaseApplier combatApplier,
        FacingPhaseApplier facingApplier,
        ReturnToCellPhaseApplier returnToCellPhaseApplier)
    {
        this.archetypeRegistry = archetypeRegistry;
        this.unitsManager = unitsManager;
        this.lineUpApplier = lineUpApplier;
        this.combatApplier = combatApplier;
        this.facingApplier = facingApplier;
        this.returnToCellPhaseApplier = returnToCellPhaseApplier;
    }


    public void Initialize()
    {
        unitsManager.OnEntityActivated += ApplyCurrentPhase;
    }


    public void Dispose()
    {
        unitsManager.OnEntityActivated -= ApplyCurrentPhase;
    }


    /// <summary>
    /// Applies the specified <paramref name="phase"/> to all <paramref name="entities"/>.
    /// Each entity's strategies are updated according to the phase.
    /// </summary>
    /// <param name="phase">The battle phase to apply (e.g., LiningUp, Combat).</param>
    /// <param name="entities">The list of entities to update.</param>
    public void ApplyPhase(
        BattlePhase phase,
        IEnumerable<BattleEntity> entities)
    {
        currentPhase = phase;
        foreach (var entity in entities)
        {
            ApplyCurrentPhase(entity);
        }
    }


    public void ApplyCurrentPhase(BattleEntity entity)
    {
        var archetypeId = entity.ArchetypeId;

        if (!archetypeRegistry.TryGet(archetypeId, out var archetype))
            throw new KeyNotFoundException();

        var baseStrategies = archetype.BaseStrategies;

        switch (currentPhase) // you store this internally
        {
            case BattlePhase.LiningUp:
                lineUpApplier.Apply(entity, baseStrategies);
                break;
            case BattlePhase.Combat:
                combatApplier.Apply(entity, baseStrategies);
                break;
            case BattlePhase.Facing:
                facingApplier.Apply(entity, baseStrategies);
                break;
            case BattlePhase.ReturningToCells:
                returnToCellPhaseApplier.Apply(entity, baseStrategies);
                break;
        }
    }
}

