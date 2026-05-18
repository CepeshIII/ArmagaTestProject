[EffectType(UnitEffectType.StrengthMultiplierEffect)]
public class StrengthMultiplierEffect : IUnitEffect
{

    public void Apply(CardInstance target, float value)
    {
        if (target is UnitCardInstance unitCardInstance)
        {
            unitCardInstance.CurrentStrength *= value;
            unitCardInstance.PropagateStatChange(
                EffectStatTarget.AttackDamage,
                value,
                EffectStackType.Multiplicative);
        }
    }


    public string GetDescription()
    {
        return "StrengthMultiplierEffect";
    }


    public EffectStatTarget GetStatTarget() => EffectStatTarget.AttackDamage;
}
