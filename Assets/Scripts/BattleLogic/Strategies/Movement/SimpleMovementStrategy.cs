using UnityEngine;
using static IsoMath;


public class SimpleMovementStrategy : IMovementStrategy
{
    public MoveData Move(Transform selfTransform, MovementData movementData, PathData pathTargetData)
    {
        var targetPosition = pathTargetData.path[^1];
        var nextPathPosition = selfTransform.position;
        if (pathTargetData.path.Length >= 1)
        {
            nextPathPosition = pathTargetData.path[0];
        }
        var distanceToNextPosition = Vector3.Distance(selfTransform.position, nextPathPosition);
        if(distanceToNextPosition <= movementData.threshold && pathTargetData.path.Length >= 2)
        {
            nextPathPosition = pathTargetData.path[1];
        }

        var distanceToTarget = Vector3.Distance(selfTransform.position, targetPosition);
        var moveDirection = (nextPathPosition - selfTransform.position).normalized;
        var deltaPosition = moveDirection * movementData.speed * Time.deltaTime;

        var isMoving = distanceToTarget >= movementData.threshold;
        if(isMoving)
        {
            selfTransform.position += deltaPosition;
        }

        return new MoveData
        {
            isMoving = isMoving,
            direction = moveDirection,
            distanceToTarget = distanceToTarget
        };
    }
}


public class CartesianMovementStrategy : IMovementStrategy
{
    public MoveData Move(Transform selfTransform, MovementData movementData, PathData pathTargetData)
    {
        var cartesianTargetPosition = pathTargetData.path[^1];
        var nextCartesianPathPosition = selfTransform.position;
        var isoSelfPosition = selfTransform.position;
        var cartesianSelfPosition = ReverseIsoProject(isoSelfPosition);

        if (pathTargetData.path.Length >= 1)
        {
            nextCartesianPathPosition = pathTargetData.path[0];
        }
        var distanceToNextPosition = Vector3.Distance(cartesianSelfPosition, nextCartesianPathPosition);
        if (distanceToNextPosition <= movementData.threshold && pathTargetData.path.Length >= 2)
        {
            nextCartesianPathPosition = pathTargetData.path[1];
        }

        var cartesianDistanceToTarget = Vector3.Distance(cartesianSelfPosition, cartesianTargetPosition);
        var cartesianMoveDirection = (nextCartesianPathPosition - (Vector3)cartesianSelfPosition).normalized;
        var cartesianDeltaPosition = cartesianMoveDirection * movementData.speed * Time.deltaTime;

        var newCartesianSelfPosition = (Vector3)cartesianSelfPosition;
        
        newCartesianSelfPosition += cartesianDeltaPosition;
        var newIsoSelfPosition = IsoProject(newCartesianSelfPosition);
        var isoDirection = newIsoSelfPosition - isoSelfPosition;

        var isMoving = cartesianDistanceToTarget >= movementData.threshold;
        if (isMoving)
        {
            selfTransform.transform.position = newIsoSelfPosition;
        }

        return new MoveData
        {
            isMoving = isMoving,
            direction = isoDirection,
            distanceToTarget = cartesianDistanceToTarget
        };
    }
}
