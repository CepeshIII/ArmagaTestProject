using System;
using UnityEngine;

[CreateAssetMenu(fileName = "RangedAttack", menuName = "Scriptable Objects/AttackDefinitions/RangedAttack")]
public class RangedAttackDefinition : AttackDefinition
{
    public override StrategyType<IAttackStrategy> ImplementationType 
        => new StrategyType<IAttackStrategy>(typeof(ProjectileAttackStrategy));
}
