using System.ComponentModel;
using Zenject;


public class GamePlayUIInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.DeclareSignal<CloseGamePlayMenu>();
        Container.DeclareSignal<OpenGamePlayMenu>();
        Container.DeclareSignal<OpenSettingsSignal>();
        Container.DeclareSignal<OpenSettingsSignal>();

        Container.Bind<MainUIGraphicRaycaster>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesAndSelfTo<UIManager>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesAndSelfTo<MenuUI>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesAndSelfTo<GameOverMenuUI>().FromComponentInHierarchy().AsSingle();


        Container.Bind<ICellInfoWindow>().To<CellInfoWindow>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesAndSelfTo<CellHoverInfoSystem>().FromNew().AsSingle();

    }
}
