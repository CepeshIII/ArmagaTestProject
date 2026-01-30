using UnityEngine;

public struct MoveToPositionIntent : IMovementIntent
{
    public Vector3 Position;

    public MoveToPositionIntent(Vector3 position)
    {
        Position = position;
    }

    public bool TryGetDestination(BattleEntity self, out Vector3 destination)
    {
        destination = Position;
        return true;
    }
}
