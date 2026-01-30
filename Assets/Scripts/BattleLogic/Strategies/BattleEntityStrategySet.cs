using System;

/// <summary>
/// Describes a complete set of strategy implementations
/// used by a <see cref="BattleEntity"/> at runtime.
/// 
/// This object is phase-aware and can be modified before being
/// installed into a DI container.
/// </summary>
public sealed class BattleEntityStrategySet
{
    private StrategyType<ICombatBehavior> combatBehavior;
    private StrategyType<IAttackStrategy> attackStrategy;
    private StrategyType<IPathFinder> pathFinder;
    private StrategyType<ITargetFinder> targetFinder;
    private StrategyType<IMovementStrategy> movementStrategy;
    private StrategyType<IFacingStrategy> facingStrategy;
    private StrategyType<IAnimationResolver> animationResolver;

    public StrategyType<ICombatBehavior> CombatBehavior => combatBehavior;
    public StrategyType<IAttackStrategy> AttackStrategy => attackStrategy;
    public StrategyType<IPathFinder> PathFinder => pathFinder;
    public StrategyType<ITargetFinder> TargetFinder => targetFinder;
    public StrategyType<IMovementStrategy> MovementStrategy => movementStrategy;
    public StrategyType<IFacingStrategy> FacingStrategy => facingStrategy;
    public StrategyType<IAnimationResolver> AnimationResolver => animationResolver;

    /// <summary>
    /// Creates an empty strategy set.
    /// </summary>
    public BattleEntityStrategySet() { }

    /// <summary>
    /// Initializes the strategy set from the entity definition.
    /// </summary>
    public BattleEntityStrategySet(BattleEntityDefinition def)
    {
        SetCombatBehavior(def.behavior.ImplementationType);
        SetAttackStrategy(def.attack.ImplementationType);
        SetPathFinder(def.pathFinder.ImplementationType);
        SetTargetFinder(def.targetFinder.ImplementationType);
        SetMovementStrategy(def.movement.ImplementationType);
        SetFacingStrategy(def.facing.ImplementationType);
        SetAnimationResolver(def.animationResolver.ImplementationType);
    }

    /// <summary>
    /// Initializes the strategy set from the entity definition.
    /// </summary>
    public BattleEntityStrategySet(BattleEntityStrategySet entityStrategySet)
    {
        combatBehavior = entityStrategySet.combatBehavior;
        attackStrategy= entityStrategySet.attackStrategy;
        pathFinder = entityStrategySet.pathFinder;
        targetFinder = entityStrategySet.targetFinder;
        movementStrategy= entityStrategySet.movementStrategy;
        facingStrategy = entityStrategySet.facingStrategy;
        animationResolver = entityStrategySet.animationResolver;
    }

    // Fluent setters (nice for phase overrides)
    public BattleEntityStrategySet SetCombatBehavior(StrategyType<ICombatBehavior> type) { combatBehavior = type; return this; }
    public BattleEntityStrategySet SetAttackStrategy(StrategyType<IAttackStrategy> type) { attackStrategy = type; return this; }
    public BattleEntityStrategySet SetPathFinder(StrategyType<IPathFinder> type) { pathFinder = type; return this; }
    public BattleEntityStrategySet SetTargetFinder(StrategyType<ITargetFinder> type) { targetFinder = type; return this; }
    public BattleEntityStrategySet SetMovementStrategy(StrategyType<IMovementStrategy> type) { movementStrategy = type; return this; }
    public BattleEntityStrategySet SetFacingStrategy(StrategyType<IFacingStrategy> type) { facingStrategy = type; return this; }
    public BattleEntityStrategySet SetAnimationResolver(StrategyType<IAnimationResolver> type) { animationResolver = type; return this; }
}