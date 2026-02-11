using UnityEngine;


public interface IBattleEntityDefinitionSource
{
    public BattleEntityDefinition BattleEntityDefinition { get; }
}


[CreateAssetMenu(menuName = "Scriptable Objects/Cards/Unit")]
public class UnitCard : CardData, IPrefabSource, IBattleEntityDefinitionSource
{
    public int baseCount;
    public float baseStrength;
    public GameObject unitPrefab;
    public BattleEntityDefinition battleEntityDefinition;

    public override CardType CardType { get { return CardType.Unit; } }

    public GameObject Prefab => unitPrefab;
    public BattleEntityDefinition BattleEntityDefinition => battleEntityDefinition;
}
