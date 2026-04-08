using UnityEngine;
using Zenject;

public class CenterPlacementStrategy : ICellPlacementStrategy
{
    private readonly ILinearGrid grid;



    [Inject]
    public CenterPlacementStrategy(ILinearGrid grid)
    {
        this.grid = grid;
    }


    public Vector3[] GetPositions(Vector2Int cellCoords, int objectCount)
    {
        var array = new Vector3[1];

        if (grid != null)
        {
            array[0] = grid.IndexCoordsToWorldCenter(cellCoords);
        }

        return array;
    }

}
