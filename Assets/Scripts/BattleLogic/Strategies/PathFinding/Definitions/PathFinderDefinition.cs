using System;
using UnityEngine;


public abstract class PathFinderDefinition : ScriptableObject, IRuntimeDefinition<IPathFinder>
{
    public abstract StrategyType<IPathFinder> ImplementationType { get; }

}
