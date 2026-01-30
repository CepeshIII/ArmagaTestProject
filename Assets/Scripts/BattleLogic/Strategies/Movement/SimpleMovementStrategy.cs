using UnityEngine;


public class SimpleMovementStrategy : IMovementStrategy
{
    public MoveData Move(Transform selfTransform, MovementData movementData, PathData pathTargetData)
    {
        var targetPosition = pathTargetData.path[^1];

        var distanceToTarget = Vector3.Distance(selfTransform.position, targetPosition);
        var moveDirection = (targetPosition - selfTransform.position).normalized;
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
