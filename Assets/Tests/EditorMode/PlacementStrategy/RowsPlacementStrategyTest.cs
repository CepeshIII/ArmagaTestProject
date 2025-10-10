using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unit tests for RowsPlacementStrategy that validate correct object placement patterns (2D version).
/// </summary>
public class RowsPlacementStrategyTests
{
    private class MockGrid : ILinearGrid
    {
        public Vector2 CellSize => new(1, 1);
        public Vector2Int GridOffset => Vector2Int.zero;
        public Vector2Int GridSize => new(10, 10);

        public void BuildGrid(GridBounds bounds) { }

        // Grid coords map directly to world coords (1:1 scale)
        public Vector2Int GridPositionToIndexCoords(Vector2Int gridPos) => gridPos;
        public Vector3 GridPositionToWorld(Vector2 gridPos) => new(gridPos.x, gridPos.y);
        public int IndexCoordsToArrayIndex(Vector2Int indexCoords) => indexCoords.y * 10 + indexCoords.x;
        public Vector2Int IndexCoordsToGridPosition(Vector2Int indexCoords) => indexCoords;
        public Vector3 IndexCoordsToWorldCenter(Vector2Int indexCoords) => new(indexCoords.x + 0.5f, indexCoords.y + 0.5f);
        public Vector3 IndexCoordsToWorldCorner(Vector2Int indexCoords) => new(indexCoords.x, indexCoords.y);
        public bool IsInsideGridIndex(Vector2Int indexCoords) => true;
        public Vector2Int WorldToGridPosition(Vector2 isoWorldPos) => Vector2Int.FloorToInt(isoWorldPos);
        public Vector2Int WorldToIndexCoords(Vector2 isoWorldPos) => Vector2Int.FloorToInt(isoWorldPos);
    }

    private RowsPlacementStrategy strategy;



    [SetUp]
    public void Setup()
    {
        var grid = /*new MockGrid(); */new LinearGrid(Vector2.one, 
            new IdentityCoordinateConverter());
        //grid.BuildFromBoundsBehaviour
        var bounds = new GridBounds 
        { 
            pointA = new Vector2(-1, 1),
            pointB = new Vector2(5, -1),
            pointC = new Vector2(5, -5),
            pointD = new Vector2(-1, -5),
        };
        grid.BuildGrid(bounds);
        strategy = new RowsPlacementStrategy(grid, maxPerRow: 5, maxPerColumn: 5, xOffset: 0.2f, yOffset: 0.2f);
    }


    [Test]
    public void GetPositions_SingleObject_Centered()
    {
        var positions = strategy.GetPositions(new Vector2Int(0, 0), 1);

        Assert.AreEqual(1, positions.Length);
        // Should be at center (0.5, 0.5)
        Assert.That(positions[0], Is.EqualTo(new Vector3(0.5f, 0.5f)));
    }


    [Test]
    public void GetPositions_TwoObjects_SymmetricalHorizontally()
    {
        var positions = strategy.GetPositions(Vector2Int.zero, 2);

        Assert.AreEqual(2, positions.Length);

        var p1 = positions[0];
        var p2 = positions[1];

        Assert.AreEqual(p1.y, p2.y, 0.0001f, "Objects should be on same row (same Y).");
        Assert.That(Mathf.Abs(p1.x - 0.5f), Is.EqualTo(Mathf.Abs(p2.x - 0.5f)).Within(0.0001f),
            "Objects should be symmetrically placed around center.");
    }


    [Test]
    public void GetPositions_FiveObjects_FormSingleRow()
    {
        var positions = strategy.GetPositions(Vector2Int.zero, 5);

        Assert.AreEqual(5, positions.Length);

        // All should have same Y (same row)
        var y = positions[0].y;
        foreach (var pos in positions)
            Assert.AreEqual(pos.y, y);
    }


    [Test]
    public void GetPositions_TenObjects_CreateTwoRows()
    {
        var positions = strategy.GetPositions(Vector2Int.zero, 10);

        Assert.AreEqual(10, positions.Length);

        var uniqueY = new HashSet<float>();
        foreach (var pos in positions)
            uniqueY.Add(pos.y);

        Assert.AreEqual(2, uniqueY.Count, "Should produce two distinct row heights (yOffset apart).");
    }


    [Test]
    public void GetPositions_ExceedMaxPerColumn_CappedAtMax()
    {
        var positions = strategy.GetPositions(Vector2Int.zero, 40);

        Assert.AreEqual(25, positions.Length);

        var uniqueY = new HashSet<float>();
        foreach (var pos in positions)
            uniqueY.Add(pos.y);

        Assert.LessOrEqual(uniqueY.Count, 5, "Should not exceed maxPerColumn rows.");
    }


    [Test]
    public void Rows_ArePlacedAboveAndBelowOrigin()
    {
        var positions = strategy.GetPositions(Vector2Int.zero, 15);

        var above = false;
        var below = false;
        foreach (var p in positions)
        {
            if (p.y > 0.5f) above = true;
            if (p.y < 0.5f) below = true;
        }

        Assert.IsTrue(above && below, "Should have rows both above and below center.");
    }


    [Test]
    public void GetPositions_ZeroObjects_ReturnsEmpty()
    {
        var positions = strategy.GetPositions(Vector2Int.zero, 0);
        Assert.IsEmpty(positions);
    }


    [Test]
    public void GetPositions_CustomMaxPerRowColumn()
    {
        var customStrategy = new RowsPlacementStrategy(new MockGrid(), maxPerRow: 3, maxPerColumn: 2);
        var positions = customStrategy.GetPositions(Vector2Int.zero, 5);

        var uniqueY = new HashSet<float>();
        foreach (var p in positions) uniqueY.Add(p.y);
        Assert.LessOrEqual(uniqueY.Count, 2);
    }

}
