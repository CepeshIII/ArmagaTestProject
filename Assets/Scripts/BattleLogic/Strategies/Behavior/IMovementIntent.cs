using UnityEngine;

public interface IMovementIntent
{
    bool TryGetDestination(out Vector3 destination);
}
