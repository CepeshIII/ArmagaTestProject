using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "EnemyWaveDefinition", menuName = "Scriptable Objects/Enemy Wave")]
public class EnemyWaveDefinition : ScriptableObject
{
    public List<EnemySpawnEntry> Enemies;
}
