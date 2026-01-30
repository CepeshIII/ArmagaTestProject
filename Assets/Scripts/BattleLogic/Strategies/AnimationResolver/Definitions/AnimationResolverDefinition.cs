using System;
using UnityEngine;


public abstract class AnimationResolverDefinition : ScriptableObject, IRuntimeDefinition<IAnimationResolver>
{
    public abstract StrategyType<IAnimationResolver> ImplementationType { get; }
}

