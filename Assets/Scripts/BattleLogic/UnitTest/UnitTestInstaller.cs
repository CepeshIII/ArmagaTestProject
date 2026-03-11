using UnityEngine;
using Zenject;



public class BattleEntityInstaller: Installer
{
    public override void InstallBindings()
    {
        // Units bindings
        Container.Bind<BattleEntity>().FromComponentInHierarchy().AsTransient();
        Container.Bind<BattleEntityAnimationEventHandler>().FromNewComponentSibling().AsTransient();
        Container.Bind<ICombatResolver>().To<CombatResolver>().FromNewComponentOnNewGameObject().AsSingle();
        Container.Bind<IBattleEntity>().FromComponentsInHierarchy().AsTransient();
        Container.Bind<ITargetFinder>().To<NearestTargetFinder>().FromNew().AsTransient();
        Container.Bind<IPathFinder>().To<StraightLinePathFinder>().FromNew().AsTransient();
        Container.Bind<ICombatBehavior>().To<SimpleMeleeBehavior>().FromNew().AsTransient();
        Container.Bind<IAttackStrategy>().To<MeleeAttackStrategy>().FromNew().AsTransient();
        Container.Bind<IAttackStrategy>().To<ProjectileAttackStrategy>().FromNew().AsTransient().When(new BindingCondition(x => x.MemberName == "Ballista"));

        Container.Bind<IMovementStrategy>().To<SimpleMovementStrategy>().FromNew().AsTransient();
        Container.Bind<IUnitAnimator>().To<SimpleUnitAnimator>().FromNew().AsTransient();

        
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
        Container.Install<BattlePhaseInstaller>();
    }
}


public class BattlePhaseInstaller: Installer
{
    public override void InstallBindings()
    {
        // Managers bindings
        Container.BindInterfacesAndSelfTo<UnitsManager>().FromNewComponentOnNewGameObject().AsSingle();
        //Container.Bind<UnitTestFactory>().FromComponentInHierarchy().AsSingle();

        Container.Bind<IDamageDisplay>().To<DamageDisplay>().FromNewComponentOnNewGameObject().AsSingle();
        Container.Bind<DamageSourceFactory>().FromNew().AsSingle();
        Container.Bind<ICombatResolver>().To<CombatResolver>().FromNewComponentOnNewGameObject().AsSingle();

        Container.Bind<AttackStrategyFactory>().FromNew().AsSingle();
        Container.Bind<CombatBehaviorFactory>().FromNew().AsSingle();
        Container.Bind<FacingFactory>().FromNew().AsSingle();
        Container.Bind<MovementStrategyFactory>().FromNew().AsSingle();
        Container.Bind<PathFinderFactory>().FromNew().AsSingle();
        Container.Bind<TargetFinderFactory>().FromNew().AsSingle();
        Container.Bind<BattleRoundController>().FromNew().AsSingle();
        Container.BindInterfacesAndSelfTo<BattleFlowController>().FromNew().AsSingle();

        Container.Bind<EnemyRoundDefinition>().FromNew().AsSingle();
        Container.Bind<EnemySpawner>().FromNew().AsSingle();
        Container.Bind<BattleEntityArchetypeRegistry>().FromNew().AsSingle();
        Container.Bind<ISpawnPositionProvider>().To<RandomCircleSpawnPositionProvider>().FromComponentInHierarchy().AsSingle();

        Container.Bind<BattleEntityFactory>()
            .AsSingle();

        Container.BindInterfacesAndSelfTo<BattleEntityController>().FromNewComponentOnNewGameObject()
            .AsSingle();

        Container.Bind<LineUpPhaseApplier>().FromNew()
            .AsSingle();

        Container.Bind<BattlePhaseApplier>().FromNew()
            .AsSingle();

        Container.BindInterfacesAndSelfTo<BattlePhaseController>().FromNew()
            .AsSingle();

        Container.Bind<FacingPhaseApplier>().FromNew()
            .AsSingle();

        Container.Bind<ReturnToCellPhaseApplier>().FromNew()
            .AsSingle();

        Container.Bind<BoardEntityRegistry>().FromNew().AsSingle();

        Container.Bind<BattleLineupPreparer>().FromNew().AsSingle();
        Container.Bind<ILineupPlacementStrategy>().To<GridLineupPlacementStrategy>().FromNew().AsSingle();
        Container.Bind<LineupEntityRegistry>().FromNew().AsSingle();

    }
}