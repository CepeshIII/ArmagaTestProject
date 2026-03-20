using UnityEngine;


public abstract class SettingUIElement<TIn, TOut> : MonoBehaviour, ISettingUIElement
{
    public abstract void Bind(SettingDefinition def, ISettingBinding<TIn, TOut> binding);

    void ISettingUIElement.Bind(SettingDefinition def, ISettingBinding binding)
    {
        Bind(def, (ISettingBinding<TIn, TOut>)binding);
    }
}


public interface ISettingUIElement
{
    void Bind(SettingDefinition def, ISettingBinding binding);
}