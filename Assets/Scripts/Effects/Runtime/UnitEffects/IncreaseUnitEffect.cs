using System;


[EffectType(UnitEffectType.IncreaseUnitEffect)]
public class IncreaseUnitEffect : IUnitEffect
{


    public void Apply(CardInstance target, float effectValue)
    {
        if (target is UnitCardInstance unitCardInstance)
            unitCardInstance.CurrentUnitCount += (int)effectValue;
    }


    public string GetDescription()
    {
        return "Increase count of Unit";
    }


    public EffectStatTarget GetStatTarget() => EffectStatTarget.None;
}

