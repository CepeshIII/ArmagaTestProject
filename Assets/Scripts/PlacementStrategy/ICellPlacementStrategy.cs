using UnityEngine;

public interface ICellPlacementStrategy
{
    /// <summary>
    /// Returns world positions for visual objects that represent this card on a specific cell.
    /// </summary>
    public Vector3[] GetPositions(Vector2Int cellCoords, int objectCount);
}
