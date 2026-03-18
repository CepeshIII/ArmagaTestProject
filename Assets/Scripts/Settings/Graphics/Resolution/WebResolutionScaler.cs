using UnityEngine;
using System.Runtime.InteropServices;


public class WebResolutionScaler: IResolutionScaler
{

#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void SetUnityResolution(int width, int height, bool isFullScreen);
#endif

    public void SetResolution(Vector2Int resolution, bool isFullScreenMode)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        SetUnityResolution(resolution.x, resolution.y, isFullScreenMode);
#endif
    }


}