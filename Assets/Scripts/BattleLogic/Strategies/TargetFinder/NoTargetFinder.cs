using UnityEngine;

public class NoTargetFinder : ITargetFinder
{
    public TargetData FindTarget(Transform transform, Team team)
    {
        return new TargetData(null, null, float.MaxValue, Vector3.zero);
    }
}
