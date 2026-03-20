using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ResolutionBinding : ISettingBinding<DropDownData, int>
{
    private readonly GraphicsSettingsManager graphics;
    private readonly SupportScreenResolutions resolutions;


    [Inject]
    public ResolutionBinding(GraphicsSettingsManager graphics, SupportScreenResolutions resolutions)
    {
        this.graphics = graphics;
        this.resolutions = resolutions;
    }


    public DropDownData GetValue()
    {
        var current = graphics.GetPendingSettings().Resolution;
        var currentIndex = 0;
        var options = new List<string>();

        for (int i = 0; i < resolutions.screenResolutions.Length; i++)
        {
            var res = resolutions.screenResolutions[i];
            int height = AspectRatioUtility.GetHeight(res.aspectRatio, res.Width);

            options.Add($"({res.aspectRatio.ToString().Remove(0, 3)}){res.Width} x {height}");

            if (current.x == res.Width && current.y == height)
            {
                currentIndex = i;
            }
        }

        return new DropDownData 
        { 
            index = currentIndex,
            strings = options
        };
    }


    public void SetValue(int value)
    {
        int index = (int)value;

        var res = resolutions.screenResolutions[index];
        int height = AspectRatioUtility.GetHeight(res.aspectRatio, res.Width);

        var settings = graphics.GetPendingSettings();
        settings.Resolution = new Vector2Int(res.Width, height);

        graphics.Change(settings);
    }

    object ISettingBinding.GetValue() => GetValue();
    void ISettingBinding.SetValue(object value) => SetValue((int)value);
}
