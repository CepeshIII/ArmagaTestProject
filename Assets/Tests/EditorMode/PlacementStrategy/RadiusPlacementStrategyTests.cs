using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/// <summary>
/// Unit tests for RowsPlacementStrategy that validate correct object placement patterns (2D version).
/// </summary>
public class RadiusPlacementStrategyTests
{
    private RadiusPlacementStrategy placementStrategy;
    private ILinearGrid linearGrid;

    [SetUp]
    public void Setup()
    {
        linearGrid = new LinearGrid(Vector2.one, new IdentityCoordinateConverter());
        placementStrategy = new RadiusPlacementStrategy(linearGrid, circleSpacing: 1f, initialObjectsPerCircle: 4, maxRadius: 2f);
    }

    [Test]
    public void Generates_CorrectNumberOfPositions()
    {
        int objectCount = 10;
        var positions = placementStrategy.GetPositions(Vector2Int.zero, objectCount);

        Assert.AreEqual(objectCount, positions.Length, "Generated position count should match requested object count.");
    }

    [Test]
    public void Positions_AreWithinMaxRadius()
    {
        int objectCount = 20;
        var positions = placementStrategy.GetPositions(Vector2Int.zero, objectCount);

        Vector2 center = new Vector2(0.5f, 0.5f);

        foreach (var pos in positions)
        {
            float distance = Vector2.Distance(new Vector2(pos.x, pos.y), center);
            Assert.LessOrEqual(distance, 2f, "All positions should be within maxRadius.");
        }
    }

    [Test]
    public void Positions_AreNotAllAtSamePoint()
    {
        int objectCount = 5;
        var positions = placementStrategy.GetPositions(Vector2Int.zero, objectCount);

        // Check that not all positions are the same
        bool allSame = positions.All(p => p == positions[0]);
        Assert.IsFalse(allSame, "Generated positions should not all be identical.");
    }
}
