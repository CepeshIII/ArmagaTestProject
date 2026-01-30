using System;

// T is the interface (e.g., IAnimationResolver)
public interface IRuntimeDefinition<T> where T : class
{
    // The property returns the wrapper for that interface
    StrategyType<T> ImplementationType { get; }
}

