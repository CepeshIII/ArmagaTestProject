public interface IEffect
{
    void Apply(CardInstance target, float value);
    string GetDescription();
}
