using System;
using UnityEngine;


public abstract class TargetFinderDefinition : ScriptableObject, IRuntimeDefinition<ITargetFinder>
{
    public abstract StrategyType<ITargetFinder> ImplementationType { get; }
}
