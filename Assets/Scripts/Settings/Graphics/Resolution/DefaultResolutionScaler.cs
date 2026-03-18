using UnityEngine;

public class DefaultResolutionScaler: IResolutionScaler
{
    public void SetResolution(Vector2Int resolution, bool isFullScreenMode)
    {
        var mode = isFullScreenMode ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
        Screen.SetResolution(resolution.x, resolution.y, mode);
    }
}
