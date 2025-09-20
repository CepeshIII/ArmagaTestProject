using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IsometricGrid>().FromComponentInHierarchy().AsSingle();

        Container.Bind<CardDeckDisplay>().FromComponentInHierarchy().AsSingle();

        Container.BindInterfacesAndSelfTo<GameBoard>().FromComponentInHierarchy().AsSingle().NonLazy();

        Container.BindInterfacesAndSelfTo<CardPlacer>().FromNewComponentOnNewGameObject().AsSingle();

        Container.BindInterfacesAndSelfTo<RoundManager>().FromNew().AsSingle().NonLazy();

        Container.BindInterfacesAndSelfTo<CardDeckController>().FromNewComponentOnNewGameObject().AsSingle();

        Container.Bind<IInitializable>().To<BoardDisplayer>().FromComponentInHierarchy().AsSingle();
    }
}
