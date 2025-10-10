using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Places objects around a central point in concentric circles.
/// </summary>
public class RadiusPlacementStrategy : ICellPlacementStrategy
{
    private readonly ILinearGrid grid;

    // Distance between consecutive circles
    private readonly float circleSpacing;

    // Number of objects in the first circle
    private readonly int initialObjectsPerCircle;

    // The maximum distance from the center at which objects will be placed.
    private readonly float maxRadius;


    public RadiusPlacementStrategy(ILinearGrid grid, float circleSpacing = 0.15f, 
        int initialObjectsPerCircle = 8, float maxRadius = 0.9f)
    {
        this.grid = grid;
        this.circleSpacing = circleSpacing;
        this.initialObjectsPerCircle = initialObjectsPerCircle;
        this.maxRadius = maxRadius;
    }


    /// <summary>
    /// Calculates world positions for a given number of objects at the specified cell coordinates.
    /// </summary>
    public Vector3[] GetPositions(Vector2Int cellCoords, int objectCount)
    {
        var positions = new Vector3[objectCount];

        // Convert cell coordinates to local grid position
        Vector2 gridCenter = grid.IndexCoordsToGridPosition(cellCoords);

        // Generate positions in concentric circles around the cell center
        var circlePositions = GenerateCircularPositions(gridCenter + new Vector2(0.5f, 0.5f), objectCount);

        // Convert grid positions to world positions
        for (int i = 0; i < circlePositions.Length; i++)
        {
            positions[i] = grid.GridPositionToWorld(circlePositions[i]);
        }

        return positions;
    }


    /// <summary>
    /// Generates positions in concentric circles around a target position.
    /// </summary>
    private Vector2[] GenerateCircularPositions(Vector2 center, int totalPositions)
    {
        var positions = new List<Vector2>();

        int objectsInCurrentCircle = 3;
        float currentRadius = circleSpacing;
        int remainingObjects = totalPositions;

        while (remainingObjects > 0 && currentRadius <= maxRadius)
        {
            // Reduce the number of objects left to place
            remainingObjects = Mathf.Max(remainingObjects - objectsInCurrentCircle, 0);

            float angleStep = 360f / objectsInCurrentCircle;
            float currentAngle = 0f;

            for (int i = 0; i < objectsInCurrentCircle; i++)
            {
                if (positions.Count == totalPositions)
                    break;

                // Calculate position based on angle and radius
                Vector2 direction = new Vector2(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad));
                positions.Add(center + direction * currentRadius);

                currentAngle += angleStep;
            }

            // Move to the next circle
            currentRadius += circleSpacing;

            // Increase objects per circle for the next layer
            objectsInCurrentCircle += initialObjectsPerCircle;
        }

        return positions.ToArray();
    }
}
