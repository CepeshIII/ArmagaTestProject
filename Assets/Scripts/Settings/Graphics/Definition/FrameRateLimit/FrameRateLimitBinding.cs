using System.Collections.Generic;
using Zenject;



public class FrameRateLimitBinding : ISettingBinding<DropDownData, int>
{
    private readonly GraphicsSettingsManager graphics;


    [Inject]
    public FrameRateLimitBinding(GraphicsSettingsManager graphics)
    {
        this.graphics = graphics;
    }


    public DropDownData GetValue() => new DropDownData
    {
        index = graphics.GetPendingSettings().FrameRateLimit,
        strings = new List<string>()
        {
            "30",
            "50",
            "60",
            "90",
            "120",
            "144"
        }
    };
        

    public void SetValue(int value)
    {
        var settings = graphics.GetPendingSettings();
        settings.FrameRateLimit = value;
        graphics.Change(settings);
    }

    object ISettingBinding.GetValue() => GetValue();
    void ISettingBinding.SetValue(object value) => SetValue((int)value);
}
