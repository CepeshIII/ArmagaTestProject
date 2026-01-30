using UnityEngine;

public interface IPathFinder
{
    public PathData FindPath(Transform selfTransform, Vector2 targetPosition);
}
