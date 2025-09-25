using System;
using UnityEngine;



[Serializable]
public class EffectData
{
    public int effectsId;
    public string effectsName;

    public EffectArea effectArea;
    public EffectTarget effectTarget;

    public UnitEffectType unitEffectType;
    public BuildingEffectType buildingEffectType;
    public float effectValue;
}

