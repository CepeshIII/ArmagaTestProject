using System;
using UnityEngine;


public abstract class CombatBehaviorDefinition : ScriptableObject, IRuntimeDefinition<ICombatBehavior>
{
    public abstract StrategyType<ICombatBehavior> ImplementationType { get; }
}


