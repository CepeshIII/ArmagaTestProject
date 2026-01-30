using System;
using UnityEngine;


public class StrategyType<T>
{
    public Type Type { get; }

    public StrategyType(Type type)
    {
        if (type != null && !typeof(T).IsAssignableFrom(type))
            throw new ArgumentException($"Type must implement {typeof(T).Name}");

        Type = type;
    }

    // Helper to make instantiation easier
    public static StrategyType<T> From<U>() where U : T
    {
        return new StrategyType<T>(typeof(U));
    }
}