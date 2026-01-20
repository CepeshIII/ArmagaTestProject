using UnityEngine;

public struct CachedGridBounds
{
    public Vector3 pointA;
    public Vector3 pointB;
    public Vector3 pointC;
    public Vector3 pointD;



    public CachedGridBounds(GridBoundsBehaviour boundsBehaviour)
    {
        var bounds = boundsBehaviour.GetGridBounds();

        pointA = bounds.pointA;
        pointB = bounds.pointB;
        pointC = bounds.pointC;
        pointD = bounds.pointD;
    }


    public bool IsEqual(GridBoundsBehaviour boundsBehaviour)
    {
        var bounds = boundsBehaviour.GetGridBounds();

        return pointA == bounds.pointA &&
                pointB == bounds.pointB &&
                pointC == bounds.pointC &&
                pointD == bounds.pointD;
    }
}
