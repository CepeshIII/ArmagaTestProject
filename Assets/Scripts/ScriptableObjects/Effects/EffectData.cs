using System;
using UnityEngine;


public abstract class EffectData : ScriptableObject
{
    public int effectsId;
    public string effectsName;
    public EffectArea area;
    public EffectTarget target;

    public abstract void ApplyEffect(CardInstance cardInstance);

}
