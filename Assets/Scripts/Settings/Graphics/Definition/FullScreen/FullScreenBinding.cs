using UnityEngine;
using Zenject;



public class FullScreenBinding : ISettingBinding<bool, bool>
{
    private readonly GraphicsSettingsManager graphics;


    [Inject]
    public FullScreenBinding(GraphicsSettingsManager graphics)
    {
        this.graphics = graphics;
    }

    public bool GetValue() => graphics.GetPendingSettings().IsFullScreen;

    public void SetValue(bool value)
    {
        var settings = graphics.GetPendingSettings();
        settings.IsFullScreen = value;
        graphics.Change(settings);
    }

    object ISettingBinding.GetValue() => GetValue();
    void ISettingBinding.SetValue(object value) => SetValue((bool)value);
}
