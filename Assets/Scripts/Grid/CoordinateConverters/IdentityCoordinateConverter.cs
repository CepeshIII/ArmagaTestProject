using UnityEngine;

public class IdentityCoordinateConverter : ICoordinateConverter
{
    public Vector3 ConvertIn(Vector3 coordinate) => coordinate;
    public Vector3 ConvertOut(Vector3 coordinate) => coordinate;
}