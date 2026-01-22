using UnityEngine;
using Zenject;


public class ProjectInstaller : MonoInstaller<ProjectInstaller>
{
    public override void InstallBindings()
    {
        // Bind the signal bus first
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<LoadSceneSignal>();
        Container.DeclareSignal<QuitFromGameSignal>();
        Container.DeclareSignal<ToMainMenuSignal>();

        Container.Bind<CardDataBase>().AsSingle();
        Container.BindInterfacesAndSelfTo<SceneLoader>().FromNewComponentOnNewGameObject().AsSingle();
        Container.BindInterfacesAndSelfTo<InputManager>().FromNew().AsSingle();
    }
}
