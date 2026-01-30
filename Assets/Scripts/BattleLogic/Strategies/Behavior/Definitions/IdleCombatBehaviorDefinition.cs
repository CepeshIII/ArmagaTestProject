using System;
using UnityEngine;

[CreateAssetMenu(fileName = "IdleCombatBehavior", menuName = "Scriptable Objects/CombatBehaviorDefinitions/IdleCombatBehavior")]
public class IdleCombatBehaviorDefinition : CombatBehaviorDefinition
{
    public override StrategyType<ICombatBehavior> ImplementationType 
        => new StrategyType<ICombatBehavior>(typeof(IdleCombatBehavior));
}
