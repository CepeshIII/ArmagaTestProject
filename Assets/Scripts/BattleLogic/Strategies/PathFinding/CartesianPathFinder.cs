using UnityEngine;
using static IsoMath;


public class CartesianPathFinder : IPathFinder
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
                new Vector3(cartesianTargetPosition.x, cartesianSelfPosition.y, 0f),
                cartesianTargetPosition
            }
        };

    }
}


