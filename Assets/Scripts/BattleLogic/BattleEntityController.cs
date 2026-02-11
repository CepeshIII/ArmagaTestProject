using UnityEngine;
using Zenject;


public class BattleEntityController: MonoBehaviour
{
    [SerializeField] private BattlePhase currentPhase;

    private BattlePhase previousPhase;
    private UnitsManager unitManager;
    private BattlePhaseController battlePhaseController;



    [Inject]
    public void Construct(UnitsManager unitManager, BattlePhaseController battlePhaseController)
    {
        this.unitManager = unitManager;
        this.battlePhaseController = battlePhaseController;
    }


    private void OnEnable()
    {
        previousPhase = currentPhase;
    }


    private void Update()
    {
        if (previousPhase != currentPhase)
        {
            previousPhase = currentPhase;

            battlePhaseController.ApplyPhase(
                currentPhase,
                unitManager.AllUnits
            );
        }
    }
}
