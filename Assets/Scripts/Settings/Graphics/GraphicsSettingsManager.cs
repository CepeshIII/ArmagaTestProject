using Zenject;


public class GraphicsSettingsManager
{
    private readonly IResolutionScaler resolutionScaler;

    private GraphicsSettingsData currentSettings;
    private GraphicsSettingsData pendingSettings;


    public GraphicsSettingsData Current => currentSettings;



    [Inject]
    public GraphicsSettingsManager()
    {
        resolutionScaler = ResolutionScalerFactory.Create();
    }



    public void Change(GraphicsSettingsData settings)
    {
        pendingSettings = settings;
        Update();
    }


    public void Apply()
    {
        currentSettings = pendingSettings;
        Update();
    }


    public void Save()
    {
        GraphicsSettingsStorage.Save(currentSettings);
    }


    public GraphicsSettingsData GetCurrentSettings()
    {
        return currentSettings;
    }


    public GraphicsSettingsData GetPendingSettings()
    {
        return pendingSettings;
    }


    public void DiscardUnappliedChanges()
    {
        // Check if the temporary 'pending' settings differ from the 'current' live settings
        if (pendingSettings.Equals(currentSettings)) return;

        pendingSettings = currentSettings;
        Update();
    }

    private void Update() 
    { 
        resolutionScaler.SetResolution(pendingSettings.Resolution, pendingSettings.IsFullScreen);
    }
}