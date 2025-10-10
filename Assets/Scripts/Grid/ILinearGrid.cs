using UnityEngine;

public interface ILinearGrid
{
    public Vector2 CellSize { get; }
    public Vector2Int GridOffset { get; }
    public Vector2Int GridSize { get; }

    public void BuildGrid(GridBounds bounds);

    public Vector2Int GridPositionToIndexCoords(Vector2Int gridPos);
    public Vector3 GridPositionToWorld(Vector2 gridPos);

    public int IndexCoordsToArrayIndex(Vector2Int indexCoords);

    public Vector2Int IndexCoordsToGridPosition(Vector2Int indexCoords);

    public Vector3 IndexCoordsToWorldCenter(Vector2Int indexCoords);
    public Vector3 IndexCoordsToWorldCorner(Vector2Int indexCoords);

    public bool IsInsideGridIndex(Vector2Int indexCoords);

    public Vector2Int WorldToGridPosition(Vector2 worldPos);
    public Vector2Int WorldToIndexCoords(Vector2 worldPos);
}
