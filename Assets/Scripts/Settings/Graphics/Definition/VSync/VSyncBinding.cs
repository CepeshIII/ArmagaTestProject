using System.Collections.Generic;
using Zenject;

public class VSyncBinding : ISettingBinding<SliderData, int>
{
    private readonly GraphicsSettingsManager graphics;


    [Inject]
    public VSyncBinding(GraphicsSettingsManager graphics)
    {
        this.graphics = graphics;
    }


    public SliderData GetValue() => new SliderData
    {
        min = 0,
        max = 4,
        value = graphics.GetPendingSettings().VSync,
    };
        

    public void SetValue(int value)
    {
        var settings = graphics.GetPendingSettings();
        settings.VSync = value;
        graphics.Change(settings);
    }

    object ISettingBinding.GetValue() => GetValue();
    void ISettingBinding.SetValue(object value) => SetValue((int)value);
}
