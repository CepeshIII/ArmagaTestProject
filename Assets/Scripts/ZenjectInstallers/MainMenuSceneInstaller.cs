using Zenject;

public class MainMenuSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<MainMenuUI>().FromComponentInHierarchy().AsSingle();
    }
}
