using System;
using System.Collections.Generic;
using UnityEngine;


public abstract class CardInstance
{
    public CardData Data { get; }
    public Vector2Int IndexCoords { get; }


    public abstract void ResetParam();
    public abstract string GetDescription();


    public CardInstance(CardData data, Vector2Int indexCoords)
    {
        Data = data;
        IndexCoords = indexCoords;
    }


    public static readonly Dictionary<CardType, Func<CardData, Vector2Int, CardInstance>> cardFactory = new()
    {
        { CardType.Unit, (data, index) => new UnitCardInstance((UnitCard)data, index) },
        { CardType.Building, (data, index) => new BuildingCardInstance((BuildingCard)data, index) },
        { CardType.Spell, (data, index) => new SpellCardInstance((SpellCard)data, index) }
    };
}


public class UnitCardInstance : CardInstance
{
    public int CurrentUnitCount { get; set; }
    public int CurrentStrength { get; set; }

    public UnitCardInstance(UnitCard data, Vector2Int indexCoords) : base(data, indexCoords)
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


public class BuildingCardInstance : CardInstance
{

    public BuildingCardInstance(BuildingCard data, Vector2Int indexCoords) : base(data, indexCoords)
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


public class SpellCardInstance : CardInstance
{

    public SpellCardInstance(SpellCard data, Vector2Int indexCoords) : base(data, indexCoords)
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

