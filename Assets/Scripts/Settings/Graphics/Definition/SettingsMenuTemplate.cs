using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Menu Template")]
public class SettingsMenuTemplate : ScriptableObject
{
    public string categoryName;
    [SerializeReference, SubclassSelector]
    public List<SettingDefinition> settings;

}