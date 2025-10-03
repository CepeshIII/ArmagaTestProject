[EffectType(UnitEffectType.StrengthMultiplierEffect)]
public class StrengthMultiplierEffect : IUnitEffect
{

    public void Apply(CardInstance target, float value)
    {
        if (target is UnitCardInstance unitCardInstance)
            unitCardInstance.CurrentStrength *= value;
    }


    public string GetDescription()
    {
        return "StrengthMultiplierEffect";
    }
}
