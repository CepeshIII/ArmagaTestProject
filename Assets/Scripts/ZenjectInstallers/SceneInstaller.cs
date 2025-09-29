using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;

public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        // Bind the signal bus first
        SignalBusInstaller.Install(Container);

        // Bind EffectFactory as a single instance
        Container.Bind<EffectFactory>().AsSingle();

        Container.Bind<InputManager>().FromComponentInHierarchy().AsSingle();

        // Bind the grid component from the scene
        Container.Bind<GridBounds>().FromComponentInHierarchy().AsSingle();

        // Bind the grid component from the scene
        Container.Bind<IsometricGrid>().FromNew().AsSingle();

        // Bind a new instance of GameBoard
        Container.Bind<GameBoard>().FromNew().AsSingle();

        // Bind factory for creating cells
        Container.BindInterfacesAndSelfTo<BoardCellsBuilder>().FromNew().AsSingle().NonLazy();

        // Bind Services
        Container.BindInterfacesAndSelfTo<DeckService>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<GridService>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<BoardService>().FromNew().AsSingle().NonLazy();

        // Bind game managers
        Container.BindInterfacesAndSelfTo<RoundManager>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<CardDeckController>().FromNewComponentOnNewGameObject().AsSingle();

        // Bind the card placer for handling card placement
        Container.BindInterfacesAndSelfTo<CardPlacer>().FromNewComponentOnNewGameObject().AsSingle();

        // Bind the deck display from the scene
        Container.Bind<CardDeckDisplay>().FromComponentInHierarchy().AsSingle();

        Container.Bind<IMaskShaderController>().To<GridShaderController>().AsCached();
        // Bind the board display and initialize it immediately
        Container.BindInterfacesAndSelfTo<BoardDisplayer>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesTo<BoardPointerChecker>();


        // Signals
        Container.DeclareSignal<BoardReadySignal>();

    }
}
