using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/UI Template Registry")]
public class UISettingsTemplate : ScriptableObject
{
    [SerializeField]
    public List<UISettingsTemplateElement> uiElements;


    public GameObject GetPrefab(UIFieldType type)
    {
        foreach (UISettingsTemplateElement element in uiElements)
        {
            if(element.fieldType == type)
            {
                return element.prefab;
            }
        }

        throw new System.Exception($"Cannot find TemplateElement for this UI FieldType: {type.ToString()}");
    }
}