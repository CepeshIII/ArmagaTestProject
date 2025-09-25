using System;

[AttributeUsage(AttributeTargets.Class)]
public class EffectTypeAttribute : Attribute
{
    public EffectTarget Target { get; }
    public UnitEffectType UnitType { get; }
    public BuildingEffectType BuildingType { get; }

    public EffectTypeAttribute(UnitEffectType unitType)
    {
        Target = EffectTarget.Unit;
        UnitType = unitType;
    }

    public EffectTypeAttribute(BuildingEffectType buildingType)
    {
        Target = EffectTarget.Building;
        BuildingType = buildingType;
    }
}