using System;

[Serializable]
public class FrameRateLimitDefinition : SettingDefinition
{
    public override UIFieldType UiType => UIFieldType.Dropdown;
    public override string DisplayName => "FrameRate Limit";
}