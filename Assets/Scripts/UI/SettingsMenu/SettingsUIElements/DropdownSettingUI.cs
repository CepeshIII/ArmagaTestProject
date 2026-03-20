using TMPro;
using UnityEngine;



public class DropdownSettingUI : SettingUIElement<DropDownData, int>
{
    [SerializeField] private TMP_Text label;
    [SerializeField] private TMP_Dropdown dropdown;

    public override void Bind(SettingDefinition defBase, 
        ISettingBinding<DropDownData, int> binding)
    {
        var data = binding.GetValue();

        label.text = defBase.DisplayName;

        dropdown.ClearOptions();
        dropdown.AddOptions(data.strings);
        dropdown.SetValueWithoutNotify(data.index);

        dropdown.onValueChanged.AddListener(index =>
        {
            binding.SetValue(index);
        });
    }
}
