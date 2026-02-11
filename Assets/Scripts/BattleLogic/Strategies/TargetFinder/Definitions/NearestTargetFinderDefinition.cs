using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NearestTargetFinder", menuName = "Scriptable Objects/TargetFinderDefinitions/NearestTargetFinder")]
public class NearestTargetFinderDefinition : TargetFinderDefinition
{
    public override StrategyType<ITargetFinder> ImplementationType 
        => StrategyType<ITargetFinder>.From<NearestTargetFinder>();
}
