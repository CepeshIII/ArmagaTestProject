
using UnityEngine;

public interface ITargetFinder
{
    public TargetData FindTarget(Transform transform, Team team);
}
