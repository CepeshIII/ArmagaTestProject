using System;
using UnityEngine;

public class SettingsUIBuilder
{
    private readonly SettingBindingFactory settingBindingFactory;
    private readonly UISettingsTemplate uiTemplate;
    private readonly SettingsContainer container;


    public SettingsUIBuilder(SettingBindingFactory settingBindingFactory, 
        UISettingsTemplate uiTemplate, SettingsContainer container)
    {
        this.settingBindingFactory = settingBindingFactory;
        this.uiTemplate = uiTemplate;
        this.container = container;
    }


    public void Build(SettingsMenuTemplate settingDefinition)
    {
        Clear();

        foreach (var setting in settingDefinition.settings)
        {
            var prefab = uiTemplate.GetPrefab(setting.UiType);
            var instance = GameObject.Instantiate(prefab, container.transform);

            if (!instance.TryGetComponent<ISettingUIElement>(out var ui))
                throw new Exception($"Prefab {prefab.name} has no ISettingUIElement");

            var binding = settingBindingFactory.Create(setting);
            ui.Bind(setting, binding);
        }
    }


    public void Clear()
    {
        foreach (Transform child in container.transform)
            GameObject.Destroy(child.gameObject);
    }
}