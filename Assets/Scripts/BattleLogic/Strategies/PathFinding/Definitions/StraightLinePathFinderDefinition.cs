using UnityEngine;


[CreateAssetMenu(fileName = "StraightLine", menuName = "Scriptable Objects/PathFinderDefinitions/StraightLine")]
public class StraightLinePathFinderDefinition : PathFinderDefinition
{
    public override StrategyType<IPathFinder> ImplementationType 
        => new StrategyType<IPathFinder>(typeof(StraightLinePathFinder));

}