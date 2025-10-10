using UnityEngine;


public class IsometricToWorldCoordinateConverter : ICoordinateConverter
{
    public Vector3 ConvertIn(Vector3 coordinate)
    {
        return IsoMath.ReverseIsoProject(coordinate);
    }

    public Vector3 ConvertOut(Vector3 coordinate)
    {
        return IsoMath.IsoProject(coordinate);
    }
}
