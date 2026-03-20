using System;
using System.Collections.Generic;

public class SettingsRepository
{
    private Dictionary<string, object> current = new();
    private Dictionary<string, object> pending = new();

    public void SetValue<T>(string id, T value)
    {
        pending[id] = value;
        OnSettingChanged?.Invoke(id, value);
    }

    public T GetValue<T>(string id)
    {
        return pending.TryGetValue(id, out var value) ? (T)value : default;
    }

    public event Action<string, object> OnSettingChanged;

    public void Apply()
    {
        current = new Dictionary<string, object>(pending);
    }

    public void Discard()
    {
        pending = new Dictionary<string, object>(current);
    }
}
