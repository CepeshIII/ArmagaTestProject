using UnityEngine;

[CreateAssetMenu(fileName = "SupportScreenResolutions", menuName = "Scriptable Objects/Settings/Graphics/SupportScreenResolutions")]
public class SupportScreenResolutions: ScriptableObject
{
    [SerializeField]
    public ScreenResolution[] screenResolutions;
}
