using UnityEngine;


public class StraightLinePathFinder : IPathFinder
{
    public PathData FindPath(Transform selfTransform, Vector2 targetPosition)
    {

        Vector3[] path = new Vector3[]
        {
            targetPosition
        };

        return new PathData
        {
            path = path
        };
    }
}

