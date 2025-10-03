using System.Collections.Generic;
using UnityEngine;

public class SpellCardInstance : CardInstance
{

    public SpellCardInstance(SpellCard data, Vector2Int indexCoords = default) : base(data, indexCoords)
    {

    }


    public override void ResetParam()
    {

    }

    public override IEnumerable<string> GetDescription()
    {
        yield return $"Name: {Data.name}";
        yield return $"{Data.description}";
    }
}

