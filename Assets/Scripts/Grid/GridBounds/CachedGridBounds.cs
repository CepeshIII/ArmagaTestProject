using UnityEngine;

public struct CachedGridBounds
{
    public Vector3 pointA;
    public Vector3 pointB;
    public Vector3 pointC;
    public Vector3 pointD;

    public CachedGridBounds(GridBoundsBehaviour bounds)
    {
        pointA = bounds.bounds.pointA;
        pointB = bounds.bounds.pointB;
        pointC = bounds.bounds.pointC;
        pointD = bounds.bounds.pointD;
    }

    public bool IsEqual(GridBoundsBehaviour bounds)
    {
        return pointA == bounds.bounds.pointA &&
                pointB == bounds.bounds.pointB &&
                pointC == bounds.bounds.pointC &&
                pointD == bounds.bounds.pointD;
    }
}
