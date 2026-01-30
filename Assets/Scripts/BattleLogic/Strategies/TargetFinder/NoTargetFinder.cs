using UnityEngine;

public class NoTargetFinder : ITargetFinder
{
    public TargetData FindTarget(Transform transform, BattleEntityData unitData)
    {
        return new TargetData(null, null, float.MaxValue, Vector3.zero);
    }
}
