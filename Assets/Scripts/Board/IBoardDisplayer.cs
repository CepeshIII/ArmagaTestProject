using UnityEngine;

public interface IBoardDisplayer
{
    public void SetCellFill(Vector2Int coord, bool isFilled);
    public void SetCellBorder(Vector2Int coord, bool visible);
}