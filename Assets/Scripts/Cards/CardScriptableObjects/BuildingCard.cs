using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Cards/Building")]
public class BuildingCard : CardData, IEffectSourceCard
{
    public GameObject buildingPrefab;
    public List<EffectData> effects;

    public override CardType CardType { get { return CardType.Building; } }


    public List<EffectData> GetEffects()
    {
        return effects;
    }


}

