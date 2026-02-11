using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SimpleMovement", menuName = "Scriptable Objects/MovementDefinitions/SimpleMovement")]
public class SimpleMovementDefinition : MovementDefinition
{
    public override StrategyType<IMovementStrategy> ImplementationType 
        => StrategyType<IMovementStrategy>.From<SimpleMovementStrategy>();
}
