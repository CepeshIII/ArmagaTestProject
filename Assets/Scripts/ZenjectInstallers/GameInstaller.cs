using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        // Bind the signal bus first
        SignalBusInstaller.Install(Container);

        Container.Bind<CardDataBase>().AsSingle();

        Container.Bind<InputManager>().FromNewComponentOnNewGameObject().AsSingle();
        Container.BindInterfacesAndSelfTo<UIManager>().FromComponentInHierarchy().AsSingle();

        Container.Install<GameStateManagerInstaller>();
        //Container.Bind<GameStateContext>().AsSingle();
        //Container.Bind<IGameStateManager>().To<GameStateManager>().FromNew().AsSingle();
        Container.BindInterfacesAndSelfTo<GameStateManager>().FromNew().AsSingle();

        Container.BindInterfacesAndSelfTo<GameManager>().FromNew().AsSingle();
        Container.Bind<CardDeckBuilder>().AsSingle();
    }
}


public class GameStateManagerInstaller : Installer
{
    public override void InstallBindings()
    {
        Container.Bind<GameStateContext>().AsSingle();

        Container.Bind<IGameState>()
            .WithId(GameState.BattlePhase)
            .To<AttackState>()
            .AsSingle();

        Container.Bind<IGameState>()
            .WithId(GameState.PreviewPhase)
            .To<PreviewState>()
            .AsSingle();

        Container.Bind<IGameState>()
            .WithId(GameState.CardPlacement)
            .To<CardPlacementState>()
            .AsSingle();

        
        Container.DeclareSignal<SwitchToNewState>();

        Container.DeclareSignal<RoundWinSignal>();
        Container.DeclareSignal<RoundLooseSignal>();
        Container.DeclareSignal<CardPlacedSignal>();
        Container.DeclareSignal<MoveIsMadeSignal>();
        Container.DeclareSignal<PlacementCompletedSignal>();

        Container.Bind<GameStateService>().FromNew().AsSingle();
    }
}
