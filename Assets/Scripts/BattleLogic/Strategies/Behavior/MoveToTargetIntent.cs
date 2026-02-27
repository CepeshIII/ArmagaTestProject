using UnityEngine;

public struct MoveToTargetIntent : IMovementIntent
{
    public BattleEntity Target;


    public MoveToTargetIntent(BattleEntity target)
    {
        Target = target;
    }


    public bool TryGetDestination(out Vector3 destination)
    {
        if (Target == null)
        {
            destination = default;
            return false;
        }

        destination = Target.transform.position;
        return true;
    }
}
