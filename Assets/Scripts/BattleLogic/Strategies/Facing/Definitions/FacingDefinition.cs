using System;
using UnityEngine;


public abstract class FacingDefinition : ScriptableObject, IRuntimeDefinition<IFacingStrategy>
{
    public abstract StrategyType<IFacingStrategy> ImplementationType { get; }
}

