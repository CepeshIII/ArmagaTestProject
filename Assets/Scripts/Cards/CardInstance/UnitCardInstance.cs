using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class UnitCardInstance : CardInstance
{
    public int CurrentUnitCount { get; set; }
    public float CurrentStrength { get; set; }


    public UnitCardInstance(UnitCard data, Vector2Int indexCoords = default) : base(data, indexCoords)
    {
        CurrentUnitCount = data.baseCount;
        CurrentStrength = data.baseStrength;
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

