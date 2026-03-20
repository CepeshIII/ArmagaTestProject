


public interface ISettingBinding
{
    object GetValue();
    void SetValue(object value);
}

public interface ISettingBinding<TIn, TOut> : ISettingBinding
{
    new TIn GetValue();
    void SetValue(TOut value);
}
