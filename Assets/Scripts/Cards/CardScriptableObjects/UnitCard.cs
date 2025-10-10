using UnityEngine;


[CreateAssetMenu(menuName = "Scriptable Objects/Cards/Unit")]
public class UnitCard : CardData, IPrefabSource
{
    public int baseCount;
    public float baseStrength;
    public GameObject unitPrefab;

    public override CardType CardType { get { return CardType.Unit; } }

    public GameObject Prefab => unitPrefab;
}
