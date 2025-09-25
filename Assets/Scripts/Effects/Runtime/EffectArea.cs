using System;
using UnityEngine;

[Serializable]
public class EffectArea
{
    public EffectAreaType areaType;
    [Range(1, 5)]
    [Min(1)]
    public int range = 1;
}

