using UnityEngine;


public class BattleSceneConfig : MonoBehaviour
{
    [Header("Rounds")]
    [SerializeField] private EnemyRoundDefinition[] rounds;
    [SerializeField] private int startRoundIndex = 0;

    public EnemyRoundDefinition[] Rounds => rounds;
    public int StartRoundIndex => startRoundIndex;


    public bool TryGetRoundDefinition(int roundIndex, out EnemyRoundDefinition roundDefinition)
    {
        if (roundIndex < 0 || roundIndex >= rounds.Length)
        {
            roundDefinition = null;
            return false;
        }
        roundDefinition = rounds[roundIndex];
        return true;
    }
}
