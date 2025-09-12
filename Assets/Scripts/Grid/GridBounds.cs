using System;
using UnityEngine;

[Serializable]
public class GridBounds : MonoBehaviour
{
    [SerializeField]
    public Color guiColour;

    [SerializeField]
    public Vector3 pointA = Vector3.zero;

    [SerializeField]
    public Vector3 pointB = new Vector3(1, 0);

    [SerializeField]
    public Vector3 pointC = new Vector3(1, -1);

    [SerializeField]
    public Vector3 pointD = new Vector3(0, -1);
}
