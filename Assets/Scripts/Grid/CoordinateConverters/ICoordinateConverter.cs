using UnityEngine;


public interface ICoordinateConverter
{
    public Vector3 ConvertIn(Vector3 coordinate);
    Vector3 ConvertOut(Vector3 coordinate);
}
