using System;
using UnityEngine;

[CreateAssetMenu(fileName = "MeleeAttack", menuName = "Scriptable Objects/AttackDefinitions/MeleeAttack")]
public class MeleeAttackDefinition : AttackDefinition
{
    public override StrategyType<IAttackStrategy> ImplementationType 
        => StrategyType<IAttackStrategy>.From<MeleeAttackStrategy>();
}

