using System;
using UnityEngine;
using Zenject;


public class BattleEntityController: MonoBehaviour
{
    [SerializeField] private int entityCount = 10;
    [SerializeField] private float radius = 10;
    [SerializeField] private BattleEntityDefinition battleEntityDefinition;
    [SerializeField] private BattlePhase currentPhase;
    [SerializeField] private Team team = Team.Enemy;

    private BattlePhase previousPhase;
    private BattleEntityFactory battleEntityFactory;
    private UnitsManager unitManager;
    private BattlePhaseController battlePhaseController;



    [Inject]
    public void Construct(UnitsManager unitManager, 
        BattleEntityFactory battleEntityFactory, BattlePhaseController battlePhaseController)
    {
        this.battleEntityFactory = battleEntityFactory;
        this.unitManager = unitManager;
        this.battlePhaseController = battlePhaseController;
    }


    private void OnEnable()
    {
        previousPhase = currentPhase;
    }


    private void Update()
    {
        while (entityCount > 0)
        {
            entityCount--;

            var randRadius = UnityEngine.Random.Range(0, radius);
            var angle = UnityEngine.Random.Range(0f, 360f) * Mathf.Deg2Rad;
            var position = transform.position + new Vector3(MathF.Cos(angle), MathF.Sin(angle)) * randRadius;

            SpawnEntity(position);
        }


        if (previousPhase != currentPhase)
        {
            previousPhase = currentPhase;

            battlePhaseController.ApplyPhase(
                currentPhase,
                unitManager.AllUnits
            );
        }
    }


    private void SpawnEntity(Vector3 position)
    {
        var unit = battleEntityFactory.Create(battleEntityDefinition, position, Quaternion.identity);
        unit.Initialize(
            new BattleEntityContext(
               new BattleEntityData { team = team},
                battleEntityDefinition.attackData,
                battleEntityDefinition.movementData,
                battleEntityDefinition.healthData
            ),
            new BattleEntityStrategySet(battleEntityDefinition)
        );

        unitManager.Register(unit.GetComponent<BattleEntity>());
    }

}
