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


    public override string GetDescription()
    {
        return $"Name: {Data.name}\n{Data.description}\nCount: {CurrentUnitCount}\nStrength: {CurrentStrength}";
    }
}

