using NUnit.Framework;
using UnityEngine;


[TestFixture]
public class IsometricGridTests
{
    private IsometricGrid grid;
    private GridBounds bounds;

    [SetUp]
    public void SetUp()
    {
        var go = new GameObject();
        bounds = go.AddComponent<GridBounds>();
        bounds.pointA = new Vector3(1, 1);
        bounds.pointC = new Vector3(8, 8);

        grid = new IsometricGrid();
        grid.BuildFromBounds(bounds);
    }

    [Test]
    public void Constructor_SetsDefaultCellSize()
    {
        var g = new IsometricGrid();
        Assert.AreEqual(new Vector2(1, 1), g.CellSize);
    }

    [Test]
    public void BuildFromBounds_SetsGridSizeAndOffset()
    {
        Assert.AreEqual(new Vector2Int(6, 6), grid.GridSize);
        Assert.AreEqual(new Vector2Int(2, 0), grid.GridOffset);
    }

    [Test]
    public void BuildGrid_InitializesGridArray()
    {
        // Should be called by BuildFromBounds, but test direct call
        grid.BuildGrid();
        Assert.AreEqual(new Vector2Int(6, 6), grid.GridSize);
    }

    [TestCase(0, 0, true)]
    [TestCase(5, 5, true)]
    [TestCase(-1, 0, false)]
    [TestCase(0, -1, false)]
    [TestCase(6, 0, false)]
    [TestCase(0, 6, false)]
    public void IsInsideGridIndex_ReturnsExpected(int x, int y, bool expected)
    {
        Assert.AreEqual(expected, grid.IsInsideGridIndex(new Vector2Int(x, y)));
    }

    [Test]
    public void WorldToGridPosition_ConvertsCorrectly()
    {
        var pos = new Vector2(2.9f, 3.9f);
        var rectCoords = IsoMath.ReverseIsoProject(pos);
        var expected = new Vector2Int(
            Mathf.FloorToInt(rectCoords.x / grid.CellSize.x),
            Mathf.FloorToInt(rectCoords.y / grid.CellSize.y)
        );
        var result = grid.WorldToGridPosition(pos);
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void WorldToIndexCoords_ConvertsCorrectly()
    {
        var pos = new Vector2(2.9f, 3.9f);
        var gridPos = grid.WorldToGridPosition(pos);
        var expected = grid.GridPositionToIndexCoords(gridPos);
        var result = grid.WorldToIndexCoords(pos);
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void IndexCoordsToArrayIndex_CalculatesCorrectly()
    {
        var index = grid.IndexCoordsToArrayIndex(new Vector2Int(2, 3));
        Assert.AreEqual(3 * 6 + 2, index);
    }

    [Test]
    public void GridPositionToIndexCoords_ConvertsCorrectly()
    {
        var gridPos = new Vector2Int(3, 2);
        var expected = gridPos - grid.GridOffset;
        expected.y = -expected.y;
        var result = grid.GridPositionToIndexCoords(gridPos);
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void IndexCoordsToGridPosition_ConvertsCorrectly()
    {
        var indexCoords = new Vector2Int(2, 3);

        var expected = indexCoords;
        expected.y = -expected.y;
        expected += grid.GridOffset;

        var result = grid.IndexCoordsToGridPosition(indexCoords);
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void GridPositionToWorld_UsesIsoMath()
    {
        var gridPos = new Vector2(1, 2);
        var expected = IsoMath.IsoProject(gridPos * grid.CellSize);
        var result = grid.GridPositionToWorld(gridPos);
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void IndexCoordsToWorldCorner_UsesGridPositionToWorld()
    {
        var indexCoords = new Vector2Int(2, 2);
        var expected = grid.GridPositionToWorld(grid.IndexCoordsToGridPosition(indexCoords));

        var result = grid.IndexCoordsToWorldCorner(indexCoords);
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void IndexCoordsToWorldCenter_UsesGridPositionToWorldWithOffset()
    {
        var indexCoords = new Vector2Int(2, 2);
        var expected = grid.GridPositionToWorld(grid.IndexCoordsToGridPosition(indexCoords));
        expected.y += grid.CellSize.y / 2f;

        var result = grid.IndexCoordsToWorldCenter(indexCoords);
        Assert.AreEqual(expected, result);
    }
}

