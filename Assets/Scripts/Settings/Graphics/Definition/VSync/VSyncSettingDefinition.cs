using System;

[Serializable]
public class VSyncSettingDefinition : SettingDefinition
{
    public override UIFieldType UiType => UIFieldType.Slider;
    public override string DisplayName => "VSync";
}