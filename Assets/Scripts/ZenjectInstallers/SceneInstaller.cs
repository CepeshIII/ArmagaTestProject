using UnityEngine;
using Zenject;


public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        // Bind Camera
        Container.Bind<MainUIGraphicRaycaster>().FromComponentInHierarchy().AsSingle();
        Container.Bind<Camera>().FromComponentOn(Camera.main.gameObject).AsSingle();

        Container.Bind<InputManager>().FromNewComponentOnNewGameObject().AsSingle();


        Container.Install<CardInstaller>();
        Container.Install<GridInstaller>();

        Container.Install<BoardInstaller>();
        Container.Install<CardPlacerInstaller>();

        Container.Install<CardDeckInstaller>();
        Container.Install<BoardDisplayerInstaller>();

        // Bind game managers
        Container.BindInterfacesAndSelfTo<RoundManager>().FromNew().AsSingle().NonLazy();
    }
}


public class CardInstaller : Installer
{
    public override void InstallBindings()
    {
        // Bind EffectFactory and CardInstanceFactory as a single instances
        Container.Bind<EffectFactory>().AsSingle();
        Container.Bind<CardInstanceFactory>().AsSingle();
    }
}


public class CardPlacerInstaller : Installer
{
    public override void InstallBindings()
    {
        // Bind the card placer for handling card placement
        Container.BindInterfacesAndSelfTo<CardPlacer>().FromNewComponentOnNewGameObject().AsSingle();
    }
}


public class CardDeckInstaller : Installer
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<CardDeckController>().FromNewComponentOnNewGameObject().AsSingle();
        
        // Bind the deck display from the scene
        Container.Bind<CardDeckDisplay>().FromComponentInHierarchy().AsSingle();

        Container.BindInterfacesAndSelfTo<DeckService>().FromNew().AsSingle().NonLazy();
    }
}


public class GridInstaller : Installer
{
    public override void InstallBindings()
    {
        // Bind the grid component from the scene
        Container.Bind<GridBounds>().FromComponentInHierarchy().AsSingle();

        // Bind the grid component from the scene
        Container.Bind<IsometricGrid>().FromNew().AsSingle();

        Container.Bind<IMaskShaderController>().To<GridShaderController>().AsCached();

        Container.BindInterfacesAndSelfTo<GridService>().FromNew().AsSingle().NonLazy();
    }
}


public class BoardInstaller : Installer
{
    public override void InstallBindings()
    {
        // Bind the signal bus first
        SignalBusInstaller.Install(Container);

        // Bind factory for creating cells
        Container.BindInterfacesAndSelfTo<BoardCellsBuilder>().FromNew().AsSingle().NonLazy();

        // Bind a new instance of GameBoard
        Container.Bind<GameBoard>().FromNew().AsSingle();

        Container.BindInterfacesAndSelfTo<BoardService>().FromNew().AsSingle().NonLazy();

        // Signals
        Container.DeclareSignal<BoardReadySignal>();
    }
}


public class BoardDisplayerInstaller : Installer
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<BoardDisplayer>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesAndSelfTo<BoardPointerTracker>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ICellInfoWindow>().To<CellInfoWindow>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesAndSelfTo<BoardHighlighter>().FromNew().AsSingle();
        Container.BindInterfacesAndSelfTo<CellHoverInfoSystem>().FromNew().AsSingle();
    }
}
