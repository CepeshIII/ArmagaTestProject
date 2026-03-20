using UnityEngine;

public struct GraphicsSettingsData
{
    public Vector2Int Resolution;
    public bool IsFullScreen;
    public int VSync;
    public int FrameRateLimit;

    public GraphicsSettingsData(Vector2Int resolution, bool fullscreen, int vSync, int frameRateLimit)
    {
        Resolution = resolution;
        IsFullScreen = fullscreen;
        VSync = vSync;
        FrameRateLimit = frameRateLimit;
    }

}
