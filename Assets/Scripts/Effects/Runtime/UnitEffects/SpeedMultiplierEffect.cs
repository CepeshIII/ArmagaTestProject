using System;

[EffectType(UnitEffectType.SpeedMultiplierEffect)]
public class SpeedMultiplierEffect : IUnitEffect
{
    public void Apply(CardInstance target, float effectValue)
    {
        throw new NotImplementedException();
    }

    public string GetDescription()
    {
        throw new NotImplementedException();
    }
}

