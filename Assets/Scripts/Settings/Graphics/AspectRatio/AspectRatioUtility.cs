using UnityEngine;

public static class AspectRatioUtility
{
    public static Vector2Int GetRatio(AspectRatio ratio)
    {
        return ratio switch
        {
            AspectRatio.AR_16x9 => new Vector2Int(16, 9),
            AspectRatio.AR_8x5 => new Vector2Int(8, 5),
            AspectRatio.AR_37x27 => new Vector2Int(37, 27),
            AspectRatio.AR_4x3 => new Vector2Int(4, 3),
            AspectRatio.AR_5x4 => new Vector2Int(5, 4),
            AspectRatio.AR_43x18 => new Vector2Int(43, 18),
            _ => new Vector2Int(16, 9)
        };
    }

    public static int GetHeight(AspectRatio ratio, int width)
    {
        var r = GetRatio(ratio);
        return Mathf.RoundToInt((float)width * r.y / r.x);
    }
}
