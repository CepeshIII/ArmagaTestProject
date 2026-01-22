using Zenject;

public class GamePlayDisplayersInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Install<CardDeckDisplayInstaller>();
        Container.Install<BoardDisplayerInstaller>();

        Container.Bind<TileMapManager>().FromComponentInHierarchy().AsSingle();

        Container.Bind<IMaskShaderController>().To<GridShaderController>().AsCached();

    }
}


public class CardDeckDisplayInstaller : Installer
{
    public override void InstallBindings()
    {
        // Bind the deck display from the scene
        Container.Bind<ICardDeckDisplay>().To<CardDeckDisplay>().FromComponentInHierarchy().AsSingle();
    }
}


public class BoardDisplayerInstaller : Installer
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<BoardDisplayer>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesAndSelfTo<BoardPointerTracker>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesAndSelfTo<BoardHighlighter>().FromNew().AsSingle();
    }
}
