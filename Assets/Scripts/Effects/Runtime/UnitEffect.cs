using System;

public interface IEffectContext
{
    public float Value { get; }
}

[Serializable]
public class UnitEffect: IEffectContext
{
    public UnitEffectType effectType;
    public float value;

    public float Value => value;
}

