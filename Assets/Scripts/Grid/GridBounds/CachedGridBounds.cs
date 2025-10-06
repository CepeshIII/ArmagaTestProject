using UnityEngine;

public struct CachedGridBounds
{
    public Vector3 pointA;
    public Vector3 pointB;
    public Vector3 pointC;
    public Vector3 pointD;

    public CachedGridBounds(GridBounds bounds)
    {
        pointA = bounds.pointA;
        pointB = bounds.pointB;
        pointC = bounds.pointC;
        pointD = bounds.pointD;
    }

    public bool IsEqual(GridBounds bounds)
    {
        return pointA == bounds.pointA &&
                pointB == bounds.pointB &&
                pointC == bounds.pointC &&
                pointD == bounds.pointD;
    }
}
