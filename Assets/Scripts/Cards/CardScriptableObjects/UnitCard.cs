using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Cards/Unit")]
public class UnitCard : CardData
{
    public int baseCount;
    public float baseStrength;
    public GameObject unitPrefab;

    public override CardType CardType { get { return CardType.Unit; } }
}
