using Zenject;


/// <summary>
/// Installs a <see cref="BattleEntityStrategySet"/> into a Zenject settingsContainer.
/// 
/// This class is responsible for binding all strategy interfaces
/// to their concrete implementations for a single entity.
/// </summary>
public static partial class BattleEntityStrategyInstaller
{
    public static void Bind(
    DiContainer container,
    BattleEntityStrategySet strategies)
    {
        container.Bind<IPathFinder>()
            .To(strategies.PathFinder.Type)
            .AsTransient();

        container.Bind<ITargetFinder>()
            .To(strategies.TargetFinder.Type)
            .AsTransient();

        container.Bind<IAttackStrategy>()
            .To(strategies.AttackStrategy.Type)
            .AsTransient();

        container.Bind<ICombatBehavior>()
            .To(strategies.CombatBehavior.Type)
            .AsTransient();

        container.Bind<IMovementStrategy>()
            .To(strategies.MovementStrategy.Type)
            .AsTransient();

        container.Bind<IFacingStrategy>()
            .To(strategies.FacingStrategy.Type)
            .AsTransient();

        container.Bind<IAnimationResolver>()
            .To(strategies.AnimationResolver.Type)
            .AsTransient();

        container.Bind<IUnitAnimator>()
            .To<SimpleUnitAnimator>()
            .AsTransient();
    }
}