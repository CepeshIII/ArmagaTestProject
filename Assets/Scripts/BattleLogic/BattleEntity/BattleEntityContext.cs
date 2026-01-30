using System;
using UnityEngine;

[Serializable]
public class BattleEntityContext
{
    [SerializeField]
    private BattleEntityData battleEntityData;
    [SerializeField]
    private AttackData attackData;
    [SerializeField] 
    private MovementData movementData;
    [SerializeField]
    private HealthData healthData;

    public BattleEntityData BattleEntityData => battleEntityData;
    public AttackData AttackData => attackData;
    public MovementData MovementData => movementData;
    public HealthData HealthData => healthData;



    public BattleEntityContext(BattleEntityData battleEntityData,
        AttackData attackData, MovementData movementData, HealthData healthData)
    {
        SetBattleEntityData(battleEntityData);
        SetAttackData(attackData);
        SetMovementData(movementData);
        SetHealthData(healthData);
    }


    public void SetBattleEntityData(BattleEntityData data)
    {
        battleEntityData = data;
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

}
