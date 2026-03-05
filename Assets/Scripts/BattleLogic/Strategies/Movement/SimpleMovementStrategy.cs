using UnityEngine;


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
        };
    }


    public bool IsAtDestination(Transform selfTransform, MovementData movementData, PathData pathTargetData)
    {
        var endPoint = pathTargetData.path[^1];
        var distance = (selfTransform.position - endPoint).magnitude;

        return distance <= movementData.threshold;
    }

}


