using System.Collections.Generic;
using UnityEngine;

public class UnitCardInstance : CardInstance
{
    public BattleEntityDefinition entityDefinition { get; set; }
    public int CurrentUnitCount { get; set; }
    public float CurrentStrength { get; set; }


    public UnitCardInstance(UnitCard data, Vector2Int indexCoords = default) : base(data, indexCoords)
    {
        CurrentUnitCount = data.baseCount;
        CurrentStrength = data.baseStrength;
        entityDefinition = data.battleEntityDefinition;
    }


    public override void ResetParam()
    {
        CurrentUnitCount = ((UnitCard)Data).baseCount;
        CurrentStrength = ((UnitCard)Data).baseStrength;
    }

    public override IEnumerable<string> GetDescription()
    {
        yield return $"Count: {CurrentUnitCount}";
        yield return $"Strength: {CurrentStrength}";
        yield return $"Description: {Data.description}";
    }
}

