using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


public class EffectFactory
{
    private readonly Dictionary<UnitEffectType, Type> unitEffectMap;
    private readonly Dictionary<BuildingEffectType, Type> buildingEffectMap;



    public EffectFactory()
    {
        // Prepare maps for effect registration
        unitEffectMap = new();
        buildingEffectMap = new();
        RegisterEffects();
    }


    /// <summary>
    /// Scan all assemblies for classes with EffectTypeAttribute and register them
    /// </summary>
    private void RegisterEffects()
    {
        // Find all effect implementations in the current assembly
        var allTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => typeof(IEffect).IsAssignableFrom(t) && !t.IsInterface);

        // Register each effect type, ensuring uniqueness
        foreach (var type in allTypes)
        {
            var attr = type.GetCustomAttribute<EffectTypeAttribute>();
            if (attr == null) continue;

            switch (attr.Target)
            {
                case EffectTarget.Unit:
                    if (unitEffectMap.ContainsKey(attr.UnitType))
                        throw new InvalidOperationException(
                            $"Duplicate effect for {attr.UnitType}: " +
                            $"{unitEffectMap[attr.UnitType].Name} vs {type.Name}");

                    unitEffectMap[attr.UnitType] = type; // register
                    break;

                case EffectTarget.Building:
                    if (buildingEffectMap.ContainsKey(attr.BuildingType))
                        throw new InvalidOperationException(
                            $"Duplicate effect for {attr.BuildingType}: " +
                            $"{buildingEffectMap[attr.BuildingType].Name} vs {type.Name}");

                    buildingEffectMap[attr.BuildingType] = type; // register
                    break;
            }
        }
    }

    /// <summary>
    /// Create effect instance for given EffectData
    /// </summary>
    public IEffect GetEffect(EffectData effectData)
    {
        switch (effectData.effectTarget)
        {
            default:
                return GetEffect(effectData.unitEffectType);
            case EffectTarget.Building:
                return GetEffect(effectData.buildingEffectType);
        }
    } 


    public IUnitEffect GetEffect(UnitEffectType type) =>
    (IUnitEffect)Activator.CreateInstance(unitEffectMap[type]);

    public IBuildingEffect GetEffect(BuildingEffectType type) =>
        (IBuildingEffect)Activator.CreateInstance(buildingEffectMap[type]);

}



