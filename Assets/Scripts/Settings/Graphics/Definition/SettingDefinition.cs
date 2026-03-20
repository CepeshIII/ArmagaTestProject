using System;


[Serializable]
public abstract class SettingDefinition
{
    public abstract UIFieldType UiType { get; }
    public abstract string DisplayName { get; }
}

