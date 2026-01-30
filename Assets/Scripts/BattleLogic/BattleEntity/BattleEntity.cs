using System;
using UnityEngine;
using Zenject;


public sealed class BattleEntity : MonoBehaviour,
    IBattleEntity, IMovable, IAttackable, IDamageable
{
    [Header("Data")] [SerializeField]
    private BattleEntityContext entityContext;

    // Cached components
    private Animator animator;

    // Strategies (runtime-swappable)
    private ITargetFinder targetFinder;
    private ICombatBehavior combatBehavior;
    private IPathFinder pathFinder;
    private IAttackStrategy attackStrategy;
    private IMovementStrategy movementStrategy;
    private IFacingStrategy facingStrategy;
    private IAnimationResolver animationResolver;

    private MoveData currentMoveData;
    private UnitIntent currentUnitIntent;
    private Vector2 currentFacing;

    // Supporting services
    private IUnitAnimator unitAnimator;
    private BattleEntityAnimationEventHandler animationEventHandler;

    private readonly AttackContext attackContext = new();

    public Vector3 EnemyBasePosition { get; internal set; }
    public BattleEntityStrategySet BaseStrategies { get; private set; }

    public event EventHandler<float> OnDamaged;
    public event EventHandler<float> OnDied;

    public BattleEntityContext Context => entityContext;
    public UnitIntent CurrentIntent => currentUnitIntent;


    #region Injection


    [Inject]
    public void Construct(
        ICombatBehavior combatBehavior,
        ITargetFinder targetFinder,
        IPathFinder pathFinder,
        IAttackStrategy attackStrategy,
        IMovementStrategy moveStrategy,
        IFacingStrategy facingStrategy,
        IUnitAnimator unitAnimator,
        IAnimationResolver animationResolver)
    {
        SetTargetFinder(targetFinder);
        SetCombatBehavior(combatBehavior);
        SetPathFinder(pathFinder);
        SetAttackStrategy(attackStrategy);
        SetMoveStrategy(moveStrategy);
        SetFacingStrategy(facingStrategy);
        SetAnimationResolver(animationResolver);

        this.unitAnimator = unitAnimator;
    }

    #endregion


    public void Initialize(BattleEntityContext battleEntityContext, BattleEntityStrategySet baseStrategies)
    {
        BaseStrategies = baseStrategies;
        entityContext = battleEntityContext;

    }


    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        animationEventHandler = gameObject.GetComponent<BattleEntityAnimationEventHandler>();

        animationEventHandler.OnAttackEvent += OnAttackHit;
    }


    private void OnDisable()
    {
        animationEventHandler.OnAttackEvent -= OnAttackHit;
    }


    private void Update()
    {
        TickBehavior();
        TickCombat();
        TickFacing();
        TickMovement();
        TickAnimation();
    }


    #region Ticks

    private void TickBehavior()
    {
        currentUnitIntent = combatBehavior.Decide(this, entityContext);
    }


    private void TickCombat()
    {
        if (currentUnitIntent.Type == IntentType.Attack)
        {
            attackStrategy.ExecuteAttack(this, attackContext);
        }

    }


    private void TickFacing()
    {
        if (facingStrategy.TryGetFacing(
            currentUnitIntent,
            currentMoveData,
            this,
            out var facing))
        {
            currentFacing = facing;
        }
    }


    private void TickMovement()
    {
        if (!currentUnitIntent.HasMovement)
            return;

        if (!currentUnitIntent.Movement.TryGetDestination(this, out var destination))
            return;

        var path = pathFinder.FindPath(transform, destination);

        currentMoveData = movementStrategy.Move(
            transform,
            entityContext.MovementData,
            path
        );
    }


    private void TickAnimation()
    {
        var animationType = animationResolver.Resolve(
            currentUnitIntent.Type,
            attackContext.phase,
            currentMoveData
        );

        unitAnimator.PlayMoveAnimation(
            animator,
            animationType,
            currentFacing
        );
    }

    #endregion


    private void OnAttackHit()
    {
        attackStrategy.OnAttackHit(this, attackContext);
    }


    public void TakeDamage(float damageAmount)
    {
        var healthData = entityContext.HealthData;
        healthData.health -= damageAmount;
        entityContext.SetHealthData(healthData);    

        OnDamaged?.Invoke(this, damageAmount);

        if (entityContext.HealthData.health <= 0f)
        {
            OnDied?.Invoke(this, 0f);
            Destroy(gameObject);
        }
    }


    #region Runtime Strategy Swapping API

    public void SetTargetFinder(ITargetFinder finder)
        => targetFinder = finder ?? throw new ArgumentNullException(nameof(finder));

    public void SetCombatBehavior(ICombatBehavior behavior)
        => combatBehavior = behavior ?? throw new ArgumentNullException(nameof(behavior));

    public void SetPathFinder(IPathFinder finder)
        => pathFinder = finder ?? throw new ArgumentNullException(nameof(finder));

    public void SetAttackStrategy(IAttackStrategy strategy)
        => attackStrategy = strategy ?? throw new ArgumentNullException(nameof(strategy));

    public void SetMoveStrategy(IMovementStrategy strategy)
        => movementStrategy = strategy ?? throw new ArgumentNullException(nameof(strategy));

    public void SetFacingStrategy(IFacingStrategy strategy)
        => facingStrategy = strategy ?? throw new ArgumentNullException(nameof(strategy));

    public void SetAnimationResolver(IAnimationResolver strategy)
        => animationResolver = strategy ?? throw new ArgumentNullException(nameof(strategy));

    #endregion
}
