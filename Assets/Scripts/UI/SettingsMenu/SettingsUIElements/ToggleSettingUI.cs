using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSettingUI : SettingUIElement<bool, bool>
{
    [SerializeField] private TMP_Text label;
    [SerializeField] private Toggle toggle;

    public Action<int> onValueChanged;


    public override void Bind(SettingDefinition def, 
        ISettingBinding<bool, bool> binding)
    {
        label.text = def.DisplayName;

        toggle.SetIsOnWithoutNotify((bool)binding.GetValue());

        toggle.onValueChanged.AddListener(value =>
        {
            binding.SetValue(value);
        });
    }

}