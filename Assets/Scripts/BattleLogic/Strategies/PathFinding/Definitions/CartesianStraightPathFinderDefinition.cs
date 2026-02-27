using UnityEngine;

[CreateAssetMenu(fileName = "CartesianStraight", menuName = "Scriptable Objects/PathFinderDefinitions/CartesianStraight")]
public class CartesianStraightPathFinderDefinition : PathFinderDefinition
{
    public override StrategyType<IPathFinder> ImplementationType
        => StrategyType<IPathFinder>.From<CartesianStraightPathFinder>();
}

