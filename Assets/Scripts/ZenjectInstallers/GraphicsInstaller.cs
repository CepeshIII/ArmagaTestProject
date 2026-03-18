using UnityEngine;
using Zenject;


public class GraphicsInstaller : MonoInstaller
{
    [SerializeField]
    private SupportScreenResolutions supportedResolutions;

    [SerializeField]
    private GraphicsSettingsDefaults graphicsSettingsDefaults;

    [SerializeField]
    private GameObject settingsMenuPrefab;


    public override void InstallBindings()
    {
        Container.DeclareSignal<OpenSettingsMenuSignal>();
        Container.DeclareSignal<CloseSettingsMenuSignal>();

        Container.BindInstance(supportedResolutions);
        Container.BindInstance(graphicsSettingsDefaults);

        Container.BindInterfacesAndSelfTo<GraphicsSettingsBootstrap>()
            .AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<GraphicsSettingsManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<SettingsMenu>().FromComponentInNewPrefab(settingsMenuPrefab).AsSingle();
        
    }
}
