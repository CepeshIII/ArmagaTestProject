using System.Collections.Generic;
using UnityEngine;

public class SpellCardInstance : CardInstance
{

    public SpellCardInstance(SpellCard data, 
        Vector2Int indexCoords = default, 
        Team team = Team.Player) : base(data, indexCoords, team)
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

