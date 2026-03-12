using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CameraBounds : MonoBehaviour
{
    [SerializeField]
    public Vector3 pointa;
    [SerializeField]
    public Color guiColour;


    public Bounds GetBounds()
    {
        var min = Vector3.Min(pointa, transform.position);
        var max = Vector3.Max(pointa, transform.position);

        return new Bounds((min + max) * 0.5f, max - min);
    }
}
