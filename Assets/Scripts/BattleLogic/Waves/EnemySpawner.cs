using Zenject;


public class EnemySpawner
{
    private readonly UnitsManager unitsManager;



    [Inject]
    public EnemySpawner(UnitsManager unitsManager)
    {
        this.unitsManager = unitsManager;
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
                entity.SetTeam(Team.Enemy);
            }
        }
    }
}