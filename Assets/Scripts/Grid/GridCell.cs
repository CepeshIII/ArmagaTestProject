using UnityEngine;

public struct GridCell
{
    public Vector2Int gridPosition;     // row/col
    public Vector2 centerRectPosition;  // center in rectangular coords
    public Vector2 centerIsoPosition;   // center in isometric coords
    public bool isOccupied;
}
