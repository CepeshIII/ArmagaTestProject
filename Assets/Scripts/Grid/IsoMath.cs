using UnityEngine;

public static class IsoMath
{
    /// <summary>
    /// Projects rectangular coordinates into isometric space.
    /// </summary>
    public static Vector3 IsoProject(Vector2 rectPos)
    {
        return new Vector3(
            rectPos.x - rectPos.y,
            (rectPos.x + rectPos.y) * 0.5f
        );
    }

    /// <summary>
    /// Reverses isometric projection back to rectangular space.
    /// </summary>
    public static Vector2 ReverseIsoProject(Vector2 isoPos)
    {
        return new Vector2(
            (isoPos.x + isoPos.y / 0.5f) * 0.5f,
            (isoPos.y / 0.5f - isoPos.x) * 0.5f
        );
    }

}