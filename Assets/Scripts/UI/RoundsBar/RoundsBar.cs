using System.Collections.Generic;
using UnityEngine;


public class RoundsBar : MonoBehaviour
{
    [SerializeField] private MyBar roundsBar;

    [SerializeField] private GameObject containerPrefab;
    [SerializeField] private Transform containersHolder;
    [SerializeField] private List<RoundIconContainer> containers = new();



    void Awake()
    {
        for (int i = 0; i < containersHolder.childCount; i++)
        {
            var child = containersHolder.GetChild(i);

            if (child != containersHolder)
            {
                Destroy(child.gameObject);
            }
        }
    }


    public void UpdateRoundsBar(BattleSceneConfig battleSceneConfig) 
    {
        PrepareContainers(battleSceneConfig.Rounds.Length, containers);
        SetDataToContainers(containers, battleSceneConfig.CurrentIndex, battleSceneConfig.Rounds);

        UpdateBar(battleSceneConfig.Rounds.Length, battleSceneConfig.CurrentIndex);
    }


    private void PrepareContainers(int count, 
        List<RoundIconContainer> containers)
    {
        var currentCount = 0;

        foreach(var container in containers)
        {
            if(container != null)
            {
                currentCount++;
            }
        }

        for(int i = currentCount; i < count; i++)
        {
            containers.Add(CreateContainer());
        }
    }


    private RoundIconContainer CreateContainer()
    {
        var newContainer = Instantiate(containerPrefab, containersHolder);
        return newContainer.GetComponent<RoundIconContainer>();
    }


    private void SetDataToContainers(List<RoundIconContainer> containers, 
        int currentRoundIndex, EnemyRoundDefinition[] rounds)
    {
        for (int i = 0; i < currentRoundIndex; i++) 
        { 
            var container = containers[i];
            var round = rounds[i];

            container.SetState(true);
            container.SetIcon(round.waveIcon);
        }

        for(int i = currentRoundIndex; i < containers.Count; i++)
        {
            var container = containers[i];
            var round = rounds[i];

            container.SetState(false);
            container.SetIcon(round.waveIcon);
        }
    }


    private void UpdateBar(int roundsCount, int currentRoundIndex)
    {
        var step = 1f / roundsCount;
        roundsBar.SetValue(step / 2f + step * currentRoundIndex);
    }
}
