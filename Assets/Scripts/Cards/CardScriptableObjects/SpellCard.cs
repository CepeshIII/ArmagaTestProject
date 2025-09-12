using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Cards/Spell")]
public class SpellCard : CardData, IEffectSourceCard
{
    public EffectData effect;

    public override CardType CardType { get { return CardType.Spell; } }

    public EffectData GetEffect()
    {
        return effect;
    }
}
