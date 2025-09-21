using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        // Bind the grid component from the scene
        Container.Bind<IsometricGrid>().FromComponentInHierarchy().AsSingle();

        // Bind a new instance of GameBoard
        Container.Bind<GameBoard>().FromNew().AsSingle();

        // Bind the board display and initialize it immediately
        Container.BindInterfacesAndSelfTo<BoardDisplayer>().FromComponentInHierarchy().AsSingle().NonLazy();

        // Bind factory for creating cells
        Container.BindInterfacesAndSelfTo<BoardCellsBuilder>().FromNew().AsSingle().NonLazy();

        // Bind game managers
        Container.BindInterfacesAndSelfTo<RoundManager>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<CardDeckController>().FromNewComponentOnNewGameObject().AsSingle();

        // Bind the card placer for handling card placement
        Container.BindInterfacesAndSelfTo<CardPlacer>().FromNewComponentOnNewGameObject().AsSingle();

        // Bind the deck display from the scene
        Container.Bind<CardDeckDisplay>().FromComponentInHierarchy().AsSingle();
    }
}
