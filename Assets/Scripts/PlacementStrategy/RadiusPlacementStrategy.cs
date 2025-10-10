using UnityEngine;

public class RadiusPlacementStrategy : ICellPlacementStrategy
{
    private readonly ILinearGrid grid;



    public RadiusPlacementStrategy(ILinearGrid grid)
    {
        this.grid = grid;
    }


    public Vector3[] GetPositions(Vector2Int cellCoords, int objectCount)
    {
        var array = new Vector3[objectCount];
        Vector2 gridPosition = grid.IndexCoordsToGridPosition(cellCoords);

        var points = GenerateMovePositionArray(gridPosition + new Vector2(0.5f, 0.5f), objectCount);

        for(var i = 0; i < points.Length; i++) 
        {
            array[i] = grid.GridPositionToWorld(points[i]);
        }

        return array;
    }

    public Vector2[] GenerateMovePositionArray(Vector2 targetPosition, int positionCount)
    {
        Vector2[] movePositionArray = new Vector2[positionCount];
        if (positionCount == 0) return movePositionArray;

        movePositionArray[0] = targetPosition;
        if (positionCount == 1) return movePositionArray;

        var ringSize = 0.1f;
        var currentIndex = 1;
        var ringNumber = 0;

        while (currentIndex < positionCount)
        {
            var countOfPositionInRing = 1 + 2 * ringNumber;
            var angleStep = 360f / countOfPositionInRing;
            var radius = ringSize + ringSize * Mathf.Log(ringNumber + 1);
            var angleOffset = UnityEngine.Random.Range(0f, 360f);

            for (int j = 0; j < countOfPositionInRing; j++)
            {
                var angle = j * angleStep + angleOffset;
                var direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                var positionOnRing = targetPosition + direction * radius;
                movePositionArray[currentIndex] = positionOnRing;
                currentIndex++;

                if (currentIndex >= positionCount - 1)
                {
                    break;
                }
            }
            ringNumber++;
        }

        return movePositionArray;
    }
}


//Rows