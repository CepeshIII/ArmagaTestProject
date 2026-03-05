using UnityEngine;


public class NoMovementStrategy : IMovementStrategy
{

    public MoveData Move(Transform selfTransform, MovementData movementData, PathData pathTargetData)
    {
        return default;
    }


    public bool IsAtDestination(Transform selfTransform, MovementData movementData, PathData pathTargetData)
    {
        return true;
    }

}
