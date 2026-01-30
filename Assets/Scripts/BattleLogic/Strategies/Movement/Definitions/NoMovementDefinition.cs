using System;
using UnityEngine;


[CreateAssetMenu(fileName = "NoMovement", menuName = "Scriptable Objects/MovementDefinitions/NoMovement")]
public class NoMovementDefinition : MovementDefinition
{
    public override StrategyType<IMovementStrategy> ImplementationType 
        => new StrategyType<IMovementStrategy>(typeof(NoMovementStrategy));
}
