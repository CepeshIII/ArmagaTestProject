using System;
using UnityEngine;


public abstract class AttackDefinition : ScriptableObject, IRuntimeDefinition<IAttackStrategy>
{
    public abstract StrategyType<IAttackStrategy> ImplementationType { get; }
}
