[EffectType(UnitEffectType.StrengthMultiplierEffect)]
public class StrengthMultiplierEffect : IUnitEffect
{
    public void Apply(CardInstance target, float value)
    {
        var unitCardInstance = (UnitCardInstance)target;
        unitCardInstance.CurrentStrength *= value;
    }

    public string GetDescription()
    {
        throw new System.NotImplementedException();
    }
}
