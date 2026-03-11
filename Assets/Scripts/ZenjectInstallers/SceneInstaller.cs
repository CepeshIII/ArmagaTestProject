using System.ComponentModel;
using UnityEngine;
using Zenject;


public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        // Bind Camera
        Container.Bind<Camera>().FromComponentOn(Camera.main.gameObject).AsSingle();

        Container.Install<CardInstaller>();
        Container.Install<GridInstaller>();

        Container.Install<BoardInstaller>();
        Container.Install<CardPlacerInstaller>();

        Container.Install<CardDeckInstaller>();

        // Bind game managers
        Container.BindInterfacesAndSelfTo<RoundManager>().FromNew().AsSingle().NonLazy();

        Container.Bind<CardViewHandlerFactory>().FromNew().AsSingle();
        Container.BindInterfacesAndSelfTo<CardPrefabFactory>().FromNew().AsSingle();
        Container.BindInterfacesAndSelfTo<CardPrefabController>().FromNew().AsSingle();

        Container.Bind<BattleSceneConfig>().FromComponentInHierarchy().AsSingle();

    }
}


public class CardInstaller : Installer
{
    public override void InstallBindings()
    {
        // Bind EffectFactory and CardInstanceFactory as a single
        Container.Bind<EffectFactory>().AsSingle();
        Container.Bind<CardInstanceFactory>().AsSingle();
    }
}


public class CardPlacerInstaller : Installer
{
    public override void InstallBindings()
    {
        // Bind the card placer for handling card placement
        Container.BindInterfacesAndSelfTo<CardPlacer>().FromNew().AsSingle();
    }
}


public class CardDeckInstaller : Installer
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<CardDeckController>().FromNew().AsSingle();
        
        Container.BindInterfacesAndSelfTo<DeckService>().FromNew().AsSingle().NonLazy();
    }
}


public class GridInstaller : Installer
{
    public override void InstallBindings()
    {
        // Bind the grid component from the scene
        Container.Bind<IGridBoundsBehaviour>().To<GridBoundsBehaviour>().FromComponentInHierarchy().AsSingle();
        // Bind the grid component from the scene
        Container.Bind<ILinearGrid>().To<LinearGrid>().FromNew().AsSingle().
            WithArguments(Vector2.one);
        Container.Bind<ICoordinateConverter>().To<IsometricToWorldCoordinateConverter>().FromNew().AsSingle();
        Container.BindInterfacesAndSelfTo<GridService>().FromNew().AsSingle().NonLazy();
    }
}


public class BoardInstaller : Installer
{
    public override void InstallBindings()
    {
        // Bind factory for creating cells
        Container.BindInterfacesAndSelfTo<BoardCellsBuilder>().FromNew().AsSingle().NonLazy();

        // Bind a new instance of GameBoard
        Container.Bind<GameBoard>().FromNew().AsSingle();

        Container.BindInterfacesAndSelfTo<BoardService>().FromNew().AsSingle().NonLazy();

        // Signals
        Container.DeclareSignal<BoardReadySignal>();
    }
}


