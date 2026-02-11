using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultAnimationResolver", menuName = "Scriptable Objects/AnimationResolverDefinitions/DefaultAnimationResolver")]
public class DefaultAnimationResolverDefinition : AnimationResolverDefinition
{
    public override StrategyType<IAnimationResolver> ImplementationType => StrategyType<IAnimationResolver>.From<DefaultAnimationResolver>();
}
