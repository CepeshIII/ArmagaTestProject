using Zenject;

/// <summary>
/// Responsible for applying a set of runtime-swappable strategies to a <see cref="BattleEntity"/>.
/// This allows dynamically changing an entity's behavior, movement, combat, and animation during different battle phases.
/// </summary>
class BattleEntityComposer
{
    /// <summary>
    /// The entity to which the strategies will be applied.
    /// </summary>
    private readonly BattleEntity battleEntity;

    // Strategies (runtime-swappable) -------------------------------------------------

    /// <summary>Handles how the entity selects and finds targets.</summary>
    private readonly ITargetFinder targetFinder;

    /// <summary>Handles the entity's combat behavior (e.g., melee, ranged).</summary>
    private readonly ICombatBehavior combatBehavior;

    /// <summary>Handles pathfinding logic for movement.</summary>
    private readonly IPathFinder pathFinder;

    /// <summary>Defines how the entity executes attacks.</summary>
    private readonly IAttackStrategy attackStrategy;

    /// <summary>Defines how the entity moves.</summary>
    private readonly IMovementStrategy movementStrategy;

    /// <summary>Defines how the entity rotates or faces targets.</summary>
    private readonly IFacingStrategy facingStrategy;

    /// <summary>Resolves animations for the entity (e.g., plays attack or movement animations).</summary>
    private IAnimationResolver animationResolver;


    /// <summary>
    /// Constructor used by Zenject to inject all required strategies and dependencies.
    /// </summary>
    /// <param displayName="battleEntity">The entity to apply strategies to.</param>
    /// <param displayName="combatBehavior">The entity's combat behavior.</param>
    /// <param displayName="targetFinder">The entity's target selection logic.</param>
    /// <param displayName="pathFinder">The entity's pathfinding logic.</param>
    /// <param displayName="attackStrategy">The entity's attack strategy.</param>
    /// <param displayName="movementStrategy">The entity's movement strategy.</param>
    /// <param displayName="facingStrategy">The entity's facing strategy.</param>
    /// <param displayName="unitAnimator">Animator interface for playing animations (injected but unused here).</param>
    /// <param displayName="animationResolver">Resolves which animations to play for actions.</param>
    [Inject]
    public BattleEntityComposer(
        BattleEntity battleEntity,
        ICombatBehavior combatBehavior,
        ITargetFinder targetFinder,
        IPathFinder pathFinder,
        IAttackStrategy attackStrategy,
        IMovementStrategy movementStrategy,
        IFacingStrategy facingStrategy,
        IUnitAnimator unitAnimator,
        IAnimationResolver animationResolver)
    {
        this.battleEntity = battleEntity;
        this.targetFinder = targetFinder;
        this.combatBehavior = combatBehavior;
        this.pathFinder = pathFinder;
        this.attackStrategy = attackStrategy;
        this.movementStrategy = movementStrategy;
        this.facingStrategy = facingStrategy;
        this.animationResolver = animationResolver;
    }


    /// <summary>
    /// Applies all injected strategies to the <see cref="BattleEntity"/>.
    /// This is typically called after a new battle phase is applied or the entity is initialized.
    /// </summary>
    public void Apply()
    {
        // Set all strategies to the BattleEntity
        battleEntity.SetTargetFinder(targetFinder);
        battleEntity.SetCombatBehavior(combatBehavior);
        battleEntity.SetPathFinder(pathFinder);
        battleEntity.SetAttackStrategy(attackStrategy);
        battleEntity.SetMoveStrategy(movementStrategy);
        battleEntity.SetFacingStrategy(facingStrategy);
        battleEntity.SetAnimationResolver(animationResolver);
    }
}
