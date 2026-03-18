using UnityEngine;

public static class ResolutionSelector
{
    public static Vector2Int GetOptimalResolution(
        SupportScreenResolutions supported)
    {
        Vector2Int screen = new Vector2Int(
            Screen.currentResolution.width,
            Screen.currentResolution.height);

        Vector2Int best = Vector2Int.zero;
        int bestPixels = 0;

        foreach (var res in supported.screenResolutions)
        {
            int height = AspectRatioUtility.GetHeight(res.aspectRatio, res.Width);
            Vector2Int candidate = new(res.Width, height);

            if (candidate.x <= screen.x &&
                candidate.y <= screen.y)
            {
                int pixels = candidate.x * candidate.y;

                if (pixels > bestPixels)
                {
                    best = candidate;
                    bestPixels = pixels;
                }
            }
        }

        // fallback
        if (best == Vector2Int.zero)
            best = screen;

        return best;
    }
}
