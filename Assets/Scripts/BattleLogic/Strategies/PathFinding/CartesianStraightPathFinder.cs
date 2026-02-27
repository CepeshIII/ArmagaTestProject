using UnityEngine;
using static IsoMath;

public class CartesianStraightPathFinder : IPathFinder
{
    public PathData FindPath(Transform selfTransform, Vector2 IsoTargetPosition)
    {
        var selfIsoPosition = selfTransform.position;

        Vector2 cartesianSelfPosition = ReverseIsoProject(selfIsoPosition);
        Vector2 cartesianTargetPosition = ReverseIsoProject(IsoTargetPosition);

        return new PathData
        {
            path = new Vector3[]
            {
                cartesianSelfPosition,
                (cartesianSelfPosition + cartesianTargetPosition) / 2f,
                cartesianTargetPosition
            }
        };

    }
}


