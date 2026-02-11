using UnityEngine;

public readonly struct LineupArea
{
    public readonly Vector2Int MinGrid;
    public readonly Vector2Int MaxGrid;

    public LineupArea(Vector2Int min, Vector2Int max)
    {
        MinGrid = min;
        MaxGrid = max;
    }
}
