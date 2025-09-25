using System;

[EffectType(UnitEffectType.IncreaseUnitEffect)]
public class IncreaseUnitEffect : IUnitEffect
{
    public void Apply(CardInstance target, float effectValue)
    {
        var unitCardInstance = target as UnitCardInstance;
        unitCardInstance.CurrentUnitCount += (int)effectValue;
    }

    public string GetDescription()
    {
        throw new NotImplementedException();
    }
}

