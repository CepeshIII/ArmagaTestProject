using System;
using UnityEngine;


public abstract class MovementDefinition : ScriptableObject, IRuntimeDefinition<IMovementStrategy>
{
    public abstract StrategyType<IMovementStrategy> ImplementationType { get; }
}


