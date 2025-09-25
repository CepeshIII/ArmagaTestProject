using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Scriptable Objects/Cards/Spell")]
public class SpellCard : CardData, IEffectSourceCard
{
    public List<EffectData> effects;

    public override CardType CardType { get { return CardType.Spell; } }

    public List<EffectData> GetEffects()
    {
        return effects;
    }
}
