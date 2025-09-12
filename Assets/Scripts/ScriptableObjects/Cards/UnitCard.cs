using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Cards/Unit")]
public class UnitCard : CardData
{
    public int baseCount;
    public int baseStrength;
    public GameObject unitPrefab;

    public override CardType CardType { get { return CardType.Unit; } }
}
