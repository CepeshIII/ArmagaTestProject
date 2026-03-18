using Zenject;
using UnityEngine;

public class GraphicsSettingsBootstrap : IInitializable
{
    private readonly GraphicsSettingsManager manager;
    private readonly GraphicsSettingsDefaults defaults;

    public GraphicsSettingsBootstrap(
        GraphicsSettingsManager manager,
        GraphicsSettingsDefaults defaults)
    {
        this.manager = manager;
        this.defaults = defaults;
    }

    public void Initialize()
    {
        var settings = GraphicsSettingsStorage.Load(defaults);
        manager.Change(settings);
        manager.Apply();
    }
}