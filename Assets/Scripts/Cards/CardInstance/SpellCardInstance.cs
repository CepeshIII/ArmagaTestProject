using UnityEngine;

public class SpellCardInstance : CardInstance
{

    public SpellCardInstance(SpellCard data, Vector2Int indexCoords = default) : base(data, indexCoords)
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

