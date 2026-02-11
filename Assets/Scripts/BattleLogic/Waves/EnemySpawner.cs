using UnityEngine;
using Zenject;


public class EnemySpawner
{
    private readonly UnitsManager unitsManager;
    private readonly ISpawnPositionProvider positionProvider;



    [Inject]
    public EnemySpawner(UnitsManager unitsManager, ISpawnPositionProvider positionProvider)
    {
        this.unitsManager = unitsManager;
        this.positionProvider = positionProvider;
    }


    public void SpawnWave(EnemyWaveDefinition wave)
    {
        foreach (var entry in wave.Enemies)
        {
            foreach(var entity in unitsManager.GetFreeEntities(
                entry.EnemyDefinition,
                entry.Count
            ))
            {
                entity.transform.position = positionProvider.GetNextPosition();
                entity.SetTeam(Team.Enemy);
            }
        }
    }
}
