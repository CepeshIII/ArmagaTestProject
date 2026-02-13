using UnityEngine;

[CreateAssetMenu(fileName = "CartesianPathFinder", menuName = "Scriptable Objects/PathFinderDefinitions/CartesianPathFinder")]
public class CartesianPathFinderDefinition : PathFinderDefinition
{
    public override StrategyType<IPathFinder> ImplementationType
        => StrategyType<IPathFinder>.From<CartesianPathFinder>();

}
