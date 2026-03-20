using System;
using TMPro;
using UnityEngine;


public class SliderSettingUI : SettingUIElement<SliderData, int>
{
    [SerializeField] private TMP_Text label;
    [SerializeField] private UnityEngine.UI.Slider slider;

    public Action<int> onValueChanged;


    public override void Bind(SettingDefinition def, 
        ISettingBinding<SliderData, int> binding)
    {
        label.text = def.DisplayName;

        var data = binding.GetValue();

        slider.wholeNumbers = true;
        slider.minValue = data.min;
        slider.maxValue = data.max;

        slider.SetValueWithoutNotify(data.value);

        slider.onValueChanged.AddListener(value =>
        {
            binding.SetValue((int)value);
        });
    }

}