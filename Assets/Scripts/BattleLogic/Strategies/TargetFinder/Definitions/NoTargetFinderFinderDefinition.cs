using UnityEngine;

[CreateAssetMenu(fileName = "NoTargetFinder", menuName = "Scriptable Objects/TargetFinderDefinitions/NoTargetFinder")]
public class NoTargetFinderFinderDefinition : TargetFinderDefinition
{
    public override StrategyType<ITargetFinder> ImplementationType 
        => StrategyType<ITargetFinder>.From<NoTargetFinder>();
}