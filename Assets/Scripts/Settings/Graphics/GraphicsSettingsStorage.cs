using UnityEngine;

public static class GraphicsSettingsStorage
{
    private const string ResX = "ResolutionX";
    private const string ResY = "ResolutionY";
    private const string Fullscreen = "Fullscreen";
    private const string VSync = "VSync";
    private const string FrameRateLimit = "FrameRateLimit";



    public static bool HasSaved()
    {
        return PlayerPrefs.HasKey(ResX);
    }


    public static GraphicsSettingsData Load(GraphicsSettingsDefaults defaults)
    {
        if (HasSaved())
        {
            var resolution = new Vector2Int(
                    PlayerPrefs.GetInt(ResX),
                    PlayerPrefs.GetInt(ResY));

            if(resolution.x > 1 && resolution.y > 1)
            {
                return new GraphicsSettingsData(
                    resolution,
                    PlayerPrefs.GetInt(Fullscreen) == 1,
                    1,
                    1);
            }
        }

        return new GraphicsSettingsData(
            defaults.defaultResolution,
            defaults.defaultFullscreen,
            defaults.VSync,
            defaults.frameRateLimit
        );
    }


    public static void Save(GraphicsSettingsData data)
    {
        PlayerPrefs.SetInt(ResX, data.Resolution.x);
        PlayerPrefs.SetInt(ResY, data.Resolution.y);
        PlayerPrefs.SetInt(Fullscreen, data.IsFullScreen ? 1 : 0);
        PlayerPrefs.SetInt(VSync, data.VSync);
        PlayerPrefs.SetInt(FrameRateLimit, data.FrameRateLimit);

        PlayerPrefs.Save();
    }
}