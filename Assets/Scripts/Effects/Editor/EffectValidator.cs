#if UNITY_EDITOR
using UnityEditor;
using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

[InitializeOnLoad]
public static class EffectValidator
{
    static EffectValidator()
    {
        ValidateEffects();
    }

    [MenuItem("Tools/Validate Effects")]
    public static void ValidateEffects()
    {
        var allTypes = AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => a.FullName.StartsWith("Assembly-CSharp")) // runtime assembly
            .SelectMany(a => a.GetTypes())
            .Where(t => typeof(IEffect).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

        var unitDict = new System.Collections.Generic.Dictionary<UnitEffectType, Type>();
        var buildingDict = new System.Collections.Generic.Dictionary<BuildingEffectType, Type>();

        foreach (var type in allTypes)
        {
            var attr = type.GetCustomAttribute<EffectTypeAttribute>();
            if (attr == null) continue;

            switch (attr.Target)
            {
                case EffectTarget.Unit:
                    if (unitDict.ContainsKey(attr.UnitType))
                    {
                        Debug.LogError(
                            $"Duplicate UnitEffectType {attr.UnitType}. " +
                            $"Existing: {unitDict[attr.UnitType].Name}, New: {type.Name}");
                    }
                    else
                    {
                        unitDict[attr.UnitType] = type;
                    }
                    break;

                case EffectTarget.Building:
                    if (buildingDict.ContainsKey(attr.BuildingType))
                    {
                        Debug.LogError(
                            $"Duplicate BuildingEffectType {attr.BuildingType}. " +
                            $"Existing: {buildingDict[attr.BuildingType].Name}, New: {type.Name}");
                    }
                    else
                    {
                        buildingDict[attr.BuildingType] = type;
                    }
                    break;
            }
        }

        foreach (var unitEffectType in (UnitEffectType[]) Enum.GetValues(typeof(UnitEffectType)))
        {
            if (!unitDict.TryGetValue(unitEffectType, out var type)) 
            {
                Debug.LogError(
                    $"No IUnitEffect class found for UnitEffectType: {unitEffectType}"
                    );
            }
        }
        foreach (var buildingEffectType in (BuildingEffectType[])Enum.GetValues(typeof(BuildingEffectType)))
        {
            if (!buildingDict.TryGetValue(buildingEffectType, out var type))
            {
                Debug.LogError(
                    $"No IBuildingEffect class found for BuildingEffectType: {buildingEffectType}"
                    );
            }
        }
    }
}
#endif
