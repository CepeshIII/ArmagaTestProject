using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyRoundDefinition", menuName = "Scriptable Objects/Enemy Round")]
public class EnemyRoundDefinition : ScriptableObject
{
    public List<EnemyWaveDefinition> Waves;
}