/// <summary>
/// Represents a runtime instance of an effect on the board,
/// containing its data and reference to the source that generated it.
/// </summary>
public class EffectInstance
{
    /// <summary>
    /// The raw effect data (value, area, type, etc.).
    /// </summary>
    public EffectData Data { get; }

    /// <summary>
    /// The source object that generated this effect (e.g., a card on the board).
    /// </summary>
    public IEffectSourceCard Source { get; }

    /// <summary>
    /// Creates a new effect instance.
    /// </summary>
    /// <param name="data">The effect data.</param>
    /// <param name="source">The source card that generated the effect.</param>
    public EffectInstance(EffectData data, IEffectSourceCard source)
    {
        Data = data;
        Source = source;
    }
}
