using System.Linq;
using UnityEngine;
using Zenject;


public class BattleRoundController
{
    private readonly BattleSceneConfig config;
    private readonly EnemySpawner enemySpawner;
    private readonly UnitsManager unitsManager;
    private readonly BattlePhaseController phaseController;
    private readonly BattleLineupPreparer battleLineupPreparer;

    public bool AllRoundsPassed => config.CurrentIndex >= config.Rounds.Length;


    [Inject]
    public BattleRoundController(
        UnitsManager unitsManager,
        EnemySpawner enemySpawner,
        BattleSceneConfig config,
        BattlePhaseController phaseController,
        BattleLineupPreparer battleLineupPreparer
        )
    { 
        this.unitsManager = unitsManager;
        this.enemySpawner = enemySpawner;
        this.config = config;
        this.phaseController = phaseController;
        this.battleLineupPreparer = battleLineupPreparer;
    }


    public void StartNextRound()
    {
        if (!config.TryGetRoundDefinition(config.CurrentIndex,
            out var roundDefinition))
        {
            throw new System.Exception($"No round definition found for wave index {config.CurrentIndex}");
        }

        foreach (var wave in roundDefinition.Waves)
        {
            enemySpawner.SpawnWave(wave);
        }

        var players = unitsManager.GetUnitsByTeam(Team.Player).ToList();
        var enemies = unitsManager.GetUnitsByTeam(Team.Enemy).ToList();
        battleLineupPreparer.Prepare(players, enemies);

        config.NextRound();
    }

}
