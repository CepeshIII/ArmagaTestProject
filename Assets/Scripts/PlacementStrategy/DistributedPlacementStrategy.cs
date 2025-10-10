using UnityEngine;



public class DistributedPlacementStrategy : ICellPlacementStrategy
{
    private readonly ILinearGrid grid;



    public DistributedPlacementStrategy(ILinearGrid grid)
    {
        this.grid = grid;
    }


    public Vector3[] GetPositions(Vector2Int cellCoords, int objectCount)
    {
        var array = new Vector3[objectCount];
        Vector2 gridPosition = grid.IndexCoordsToGridPosition(cellCoords);

        var sqrtCount = Mathf.CeilToInt(Mathf.Sqrt(objectCount));
        var step = 1f / (float)sqrtCount;

        var i = 0;
        for (int x = 0; x < sqrtCount; x++)
        {
            for (int y = 0; y < sqrtCount; y++)
            {
                var currentPosition = gridPosition + new Vector2(x * step, y * step);
                array[i] = grid.GridPositionToWorld(currentPosition);
                
                i++;
                if(i >= objectCount) break;
            }
            if (i >= objectCount) break;
        }
        array[0] = grid.GridPositionToWorld(gridPosition + new Vector2(0, 0));
        array[1] = grid.GridPositionToWorld(gridPosition + new Vector2(1, 0));
        array[2] = grid.GridPositionToWorld(gridPosition + new Vector2(1, 1));
        array[3] = grid.GridPositionToWorld(gridPosition + new Vector2(0, 1));

        return array;
    }
}
