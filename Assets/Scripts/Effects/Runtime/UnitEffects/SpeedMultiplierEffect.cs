using System;


[EffectType(UnitEffectType.SpeedMultiplierEffect)]
public class SpeedMultiplierEffect : IUnitEffect
{
    public void Apply(CardInstance target, float value)
    {
        if (target is UnitCardInstance unitCardInstance)
        {
            unitCardInstance.PropagateStatChange(
                EffectStatTarget.Speed,
                value,
                EffectStackType.Multiplicative);
        }
    }


    public string GetDescription()
    {
        return "SpeedMultiplierEffect";
    }


    public EffectStatTarget GetStatTarget() => EffectStatTarget.Speed;
}
