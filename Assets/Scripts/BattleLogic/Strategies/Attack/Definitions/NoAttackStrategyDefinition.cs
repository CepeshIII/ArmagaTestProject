using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NoAttackStrategy", menuName = "Scriptable Objects/AttackDefinitions/NoAttackStrategy")]
public class NoAttackStrategyDefinition : AttackDefinition
{
    public override StrategyType<IAttackStrategy> ImplementationType 
        => new StrategyType<IAttackStrategy>(typeof(NoAttackStrategy));
}

