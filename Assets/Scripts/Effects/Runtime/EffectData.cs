using System;
using UnityEngine;



[Serializable]
public class EffectData
{
    public int effectsId;
    public string effectsName;

    public EffectArea effectArea;
    //public EffectTarget effectTarget;

    public UnitEffectType unitEffectType;
    public BuildingEffectType buildingEffectType;
    public float effectValue;

    [SerializeField]
    public EffectFilter filter;

    public EffectStackType stackType;
}


public enum EffectStackType
{
    Additive,
    Multiplicative,
    Override
}
