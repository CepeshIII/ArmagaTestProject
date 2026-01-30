using UnityEngine;

[CreateAssetMenu(fileName = "NoTargetFinder", menuName = "Scriptable Objects/TargetFinderDefinitions/NoTargetFinder")]
public class NoTargetFinderFinderDefinition : TargetFinderDefinition
{
    public override StrategyType<ITargetFinder> ImplementationType 
        => new StrategyType<ITargetFinder>(typeof(NoTargetFinder));
}