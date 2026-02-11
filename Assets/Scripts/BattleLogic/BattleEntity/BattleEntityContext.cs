using System;
using UnityEngine;


[Serializable]
public class BattleEntityContext
{
    [SerializeField]
    private AttackData attackData;
    [SerializeField] 
    private MovementData movementData;
    [SerializeField]
    private HealthData healthData;

    public AttackData AttackData => attackData;
    public MovementData MovementData => movementData;
    public HealthData HealthData => healthData;



    public BattleEntityContext(AttackData attackData, 
        MovementData movementData, HealthData healthData)
    {
        SetAttackData(attackData);
        SetMovementData(movementData);
        SetHealthData(healthData);
    }


    public void SetAttackData(AttackData data)
    {
        attackData = data;
    }


    public void SetMovementData(MovementData data)
    {
        movementData = data;
    }


    public void SetHealthData(HealthData data)
    {
        healthData = data;
    }


    public BattleEntityContext Clone()
    {
        return new BattleEntityContext(
            attackData,
            movementData,
            healthData
        );
    }

}
