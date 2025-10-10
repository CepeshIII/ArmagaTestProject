using System;
using UnityEngine;


[Serializable]
public class GridBoundsBehaviour : MonoBehaviour
{
    [SerializeField]
    public Color guiColour;

    [SerializeField]
    public GridBounds bounds = new();
}
