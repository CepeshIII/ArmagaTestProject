using System;

[Serializable]
public class FullScreenSettingDefinition: SettingDefinition
{
    public override UIFieldType UiType { get { return UIFieldType.Toggle; } }
    public override string DisplayName { get { return "IsFullScreen"; } }
}

