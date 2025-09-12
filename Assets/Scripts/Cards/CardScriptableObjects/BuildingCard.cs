using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Cards/Building")]
public class BuildingCard : CardData, IEffectSourceCard
{
    public GameObject buildingPrefab;
    public EffectData effect;

    public override CardType CardType { get { return CardType.Building; } }

    public EffectData GetEffect()
    {
        return effect;
    }


}
