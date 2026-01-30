
using UnityEngine;

public interface ITargetFinder
{
    public TargetData FindTarget(Transform transform, BattleEntityData unitData);
}
