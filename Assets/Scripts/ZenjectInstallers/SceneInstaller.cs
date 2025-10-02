using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;

public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        // Bind the signal bus first
        SignalBusInstaller.Install(Container);

        Container.Bind<Camera>().FromComponentOn(Camera.main.gameObject).AsSingle();

        // Bind EffectFactory and CardInstanceFactory as a single instances
        Container.Bind<EffectFactory>().AsSingle();
        Container.Bind<CardInstanceFactory>().AsSingle();

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
        Container.BindInterfacesAndSelfTo<BoardPointerTracker>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ICellInfoWindow>().To<CellInfoWindow>().FromComponentInHierarchy().AsSingle();

        Container.BindInterfacesAndSelfTo<BoardHighlighter>().FromNew().AsSingle();
        Container.BindInterfacesAndSelfTo<CellHoverInfoSystem>().FromNew().AsSingle();


        // Signals
        Container.DeclareSignal<BoardReadySignal>();

    }
}
