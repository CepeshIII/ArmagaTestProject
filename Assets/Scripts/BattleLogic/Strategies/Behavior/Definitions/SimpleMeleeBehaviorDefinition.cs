using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SimpleMeleeBehavior", menuName = "Scriptable Objects/CombatBehaviorDefinitions/SimpleMeleeBehavior")]
public class SimpleMeleeBehaviorDefinition : CombatBehaviorDefinition
{
    public override StrategyType<ICombatBehavior> ImplementationType 
        => StrategyType<ICombatBehavior>.From<SimpleMeleeBehavior>();
}
