using System;
using UnityEngine;


public interface IGridBoundsBehaviour
{
    public void SetGridBounds(GridBounds gridBounds);
    public void SetColor(Color color);

    public GridBounds GetGridBounds();
    public Color GetColor();


}