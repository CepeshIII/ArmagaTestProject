using System.ComponentModel;
using UnityEngine;
using Zenject;



public class BattleEntityInstaller: Installer
{
    public override void InstallBindings()
    {
        // Units bindings
        Container.Bind<BattleEntity>().FromComponentInHierarchy().AsTransient();
        Container.Bind<BattleEntityAnimationEventHandler>().FromNewComponentSibling().AsTransient();
        Container.Bind<IDamageManager>().To<DamageManager>().FromNewComponentOnNewGameObject().AsSingle();
        Container.Bind<IBattleEntity>().FromComponentsInHierarchy().AsTransient();
        Container.Bind<ITargetFinder>().To<NearestTargetFinder>().FromNew().AsTransient();
        Container.Bind<IPathFinder>().To<StraightLinePathFinder>().FromNew().AsTransient();
        Container.Bind<ICombatBehavior>().To<SimpleMeleeBehavior>().FromNew().AsTransient();
        Container.Bind<IAttackStrategy>().To<MeleeAttackStrategy>().FromNew().AsTransient();
        Container.Bind<IAttackStrategy>().To<ProjectileAttackStrategy>().FromNew().AsTransient().When(new BindingCondition(x => x.MemberName == "Ballista"));

        Container.Bind<IMovementStrategy>().To<SimpleMovementStrategy>().FromNew().AsTransient();
        Container.Bind<IUnitAnimator>().To<SimpleUnitAnimator>().FromNew().AsTransient();

        Container.Bind<BattleEntityData>().FromMethodUntyped(ctx =>
        {
            return new BattleEntityData
            {
                team = Team.Player,
            };
        }).AsTransient();

        Container.Bind<MovementData>().FromMethodUntyped(ctx =>
        {
            return new MovementData
            {
                speed = 1f,
                acceleration = 10f,
                threshold = 2f
            };
        }).AsTransient();

        Container.Bind<AttackData>().FromMethodUntyped(ctx =>
        {
            return new AttackData
            {
                attackDistance = 2f,
                offset = 1,
                radius = 0.75f,
                attackDamage = 25f,
                rechargeTime = 1,
            };

        }).AsTransient();


        Container.Bind<HealthData>().FromMethodUntyped((System.Func<InjectContext, object>)(ctx =>
        {
            return new HealthData
            {
                health = 100f,
            };
        })).AsTransient();
    }
}


[CreateAssetMenu(fileName = "UnitTestInstaller", menuName = "Installers/UnitTestInstaller")]
public class UnitTestInstaller : ScriptableObjectInstaller<UnitTestInstaller>
{
    public override void InstallBindings()
    {
        // Managers bindings
        Container.BindInterfacesAndSelfTo<UnitsManager>().FromNewComponentOnNewGameObject().AsSingle();
        Container.Bind<UnitTestFactory>().FromComponentInHierarchy().AsSingle();

        Container.Bind<IDamageDisplay>().To<DamageDisplay>().FromNewComponentOnNewGameObject().AsSingle();
        Container.Bind<DamageSourceFactory>().FromNew().AsSingle();
        Container.Bind<IDamageManager>().To<DamageManager>().FromNewComponentOnNewGameObject().AsSingle();

        //Container.Install<BattleEntityInstaller>();


        Container.Bind<AttackStrategyFactory>().FromNew().AsSingle();
        Container.Bind<CombatBehaviorFactory>().FromNew().AsSingle();
        Container.Bind<FacingFactory>().FromNew().AsSingle();
        Container.Bind<MovementStrategyFactory>().FromNew().AsSingle();
        Container.Bind<PathFinderFactory>().FromNew().AsSingle();
        Container.Bind<TargetFinderFactory>().FromNew().AsSingle();

        Container.Bind<BattleEntityFactory>()
            .AsSingle();

        Container.Bind<BattleEntityController>().FromComponentInHierarchy()
            .AsSingle();

        Container.Bind<LineUpPhaseApplier>().FromNew()
            .AsSingle();

        Container.Bind<BattlePhaseApplier>().FromNew()
            .AsSingle();

        Container.Bind<BattlePhaseController>().FromNew()
            .AsSingle();

        Container.BindInterfacesAndSelfTo<LineupProvider>().FromNewComponentOnNewGameObject()
            .AsSingle();

    }
}