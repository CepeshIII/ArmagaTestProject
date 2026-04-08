using System;
using UnityEngine;

[Serializable]
public class EffectFilter
{
    [SerializeField]
    public EffectTarget targetType = EffectTarget.Unit;

    [SerializeField]
    public EffectTeamFilter teamFilter = EffectTeamFilter.Any;
}
