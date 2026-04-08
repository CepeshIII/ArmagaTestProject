using System.Collections.Generic;
using UnityEngine;

public class BuildingCardInstance : CardInstance
{
    public BattleEntityDefinition entityDefinition { get; set; }


    public BuildingCardInstance(BuildingCard data, Vector2Int indexCoords = default, Team team = Team.Player)
        : base(data, indexCoords, team)
    {
        entityDefinition = data.battleEntityDefinition;
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

