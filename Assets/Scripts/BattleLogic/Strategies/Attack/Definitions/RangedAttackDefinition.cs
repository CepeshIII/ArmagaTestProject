using System;
using UnityEngine;

[CreateAssetMenu(fileName = "RangedAttack", menuName = "Scriptable Objects/AttackDefinitions/RangedAttack")]
public class RangedAttackDefinition : AttackDefinition
{
    public override StrategyType<IAttackStrategy> ImplementationType 
        => StrategyType<IAttackStrategy>.From<ProjectileAttackStrategy>();
}
