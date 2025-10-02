using UnityEngine;

public class BuildingCardInstance : CardInstance
{

    public BuildingCardInstance(BuildingCard data, Vector2Int indexCoords = default) : base(data, indexCoords)
    {

    }


    public override void ResetParam()
    {

    }

    public override string GetDescription()
    {
        return $"Name: {Data.name}\n{Data.description}";
    }
}

