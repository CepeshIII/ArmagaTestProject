using UnityEngine;
using Zenject;

/// <summary>
/// Factory responsible for creating <see cref="BattleEntity"/> instances.
/// 
/// It creates a dedicated sub-container per entity, binds all runtime strategies
/// based on the entity definition, and instantiates the entity prefab with
/// those dependencies injected.
/// </summary>
public class BattleEntityFactory
{
    private readonly DiContainer container;

    // Legacy / specialized factories (kept for reference or future refactor)
    private readonly AttackStrategyFactory attackStrategyFactory;
    private readonly CombatBehaviorFactory combatBehaviorFactory;
    private readonly FacingFactory facingFactory;
    private readonly MovementStrategyFactory movementStrategy;
    private readonly PathFinderFactory pathFinderFactory;
    private readonly TargetFinderFactory targetFinderFactory;

    [Inject]
    public BattleEntityFactory(
        AttackStrategyFactory attackStrategyFactory,
        CombatBehaviorFactory combatBehaviorFactory,
        FacingFactory facingFactory,
        MovementStrategyFactory movementDefinitionFactory,
        PathFinderFactory pathFinderFactory,
        TargetFinderFactory targetFinderFactory,
        DiContainer container)
    {
        this.container = container;
        this.attackStrategyFactory = attackStrategyFactory;
        this.combatBehaviorFactory = combatBehaviorFactory;
        this.facingFactory = facingFactory;
        this.movementStrategy = movementDefinitionFactory;
        this.pathFinderFactory = pathFinderFactory;
        this.targetFinderFactory = targetFinderFactory;
    }

    /// <summary>
    /// Creates a battle entity at the given position using the provided definition.
    /// 
    /// A sub-container is created per entity so that strategies are isolated
    /// and can be swapped per phase without affecting other entities.
    /// </summary>
    public BattleEntity Create(
        BattleEntityDefinition definition,
        Vector3 position,
        Quaternion rotation)
    {
        var subContainer = container.CreateSubContainer();

        // Build strategy set from definition and bind it to the sub-container
        var strategies = definition.GetStrategySet();
        var defaultStrategies = new BattleEntityStrategySet(strategies);
        //defaultStrategies.SetCombatBehavior(StrategyType<ICombatBehavior>.From<IdleCombatBehavior>());
        //defaultStrategies.SetFacingStrategy(StrategyType<IFacingStrategy>.From<StaticFacingStrategy>());
        BattleEntityStrategyInstaller.Bind(subContainer, defaultStrategies);

        // Instantiate the entity prefab with all dependencies injected
        var entity = subContainer.InstantiatePrefabForComponent<BattleEntity>(
            definition.prefab,
            position,
            rotation,
            null
        );

        var entityContext = new BattleEntityContext(
            definition.attackData,
            definition.movementData,
            definition.healthData
        );

        // Initialize entity-specific data (stats, visuals, etc.)
        entity.Initialize(definition.GetInstanceID(), entityContext);

        return entity;
    }
}