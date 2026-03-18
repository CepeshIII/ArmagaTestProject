using UnityEngine;

public struct GraphicsSettingsData
{
    public Vector2Int Resolution;
    public bool IsFullScreen;

    public GraphicsSettingsData(Vector2Int resolution, bool fullscreen)
    {
        Resolution = resolution;
        IsFullScreen = fullscreen;
    }
}
