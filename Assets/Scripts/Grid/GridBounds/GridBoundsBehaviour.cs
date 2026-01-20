using System;
using UnityEngine;


[Serializable]
public class GridBoundsBehaviour : MonoBehaviour, IGridBoundsBehaviour
{
    [SerializeField]
    private Color guiColour;

    [SerializeField]
    private GridBounds bounds = new();



    public void SetGridBounds(GridBounds gridBounds)
    {
        bounds = gridBounds;
    }


    public GridBounds GetGridBounds()
    {
        return bounds;
    }
    

    public void SetColor(Color color)
    {
        guiColour = color;
    }


    public Color GetColor()
    {
       return guiColour;
    }
}
