using System;
using System.Runtime.Serialization;

[Serializable]
public class ResolutionSettingDefinition : SettingDefinition
{
    public override UIFieldType UiType => UIFieldType.Dropdown;
    public override string DisplayName => "Resolution";
}
