using UnityEngine;

public interface IMovementStrategy
{
    public MoveData Move(Transform selfTransform, MovementData movementData, PathData pathTargetData);
    public bool IsAtDestination(Transform selfTransform, MovementData movementData, PathData pathTargetData);
}
