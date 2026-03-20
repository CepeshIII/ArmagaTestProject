using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Graphics Default Settings")]
public class GraphicsSettingsDefaults : ScriptableObject
{
    public Vector2Int defaultResolution = new(1920, 1080);
    public bool defaultFullscreen = true;
    public int VSync = 1;
    public int frameRateLimit = 1;
}