using UnityEngine;

[CreateAssetMenu(fileName = "CartesianMovement", menuName = "Scriptable Objects/MovementDefinitions/CartesianMovement")]
public class CartesianMovementStrategyDefinition : MovementDefinition
{
    public override StrategyType<IMovementStrategy> ImplementationType
        => StrategyType<IMovementStrategy>.From<CartesianMovementStrategy>();
}
