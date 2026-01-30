using UnityEngine;

public interface IMovementIntent
{
    bool TryGetDestination(BattleEntity self, out Vector3 destination);
}
