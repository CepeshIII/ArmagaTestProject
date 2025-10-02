using UnityEngine;

public class BoardCellsBuilder
{
    private readonly IsometricGrid grid;


    public BoardCellsBuilder(IsometricGrid grid)
    {
        this.grid = grid;
    }


    public Cell[] CreateCells()
    {
        var board = new Cell[grid.GridSize.x * grid.GridSize.y];

        int index = 0;
        for (int y = 0; y < grid.GridSize.y; y++)
        {
            for (int x = 0; x < grid.GridSize.x; x++)
            {
                board[index] = new Cell
                {
                    cards = new(),
                    effects = new(),
                    isAvailable = false,
                    indexCoord = new Vector2Int(x, y)
                };
                index++;
            }
        }

        return board;
    }


    public void SetAvailableCells(Cell[] board, Vector2Int[] availableCoords)
    {
        foreach (var coord in availableCoords)
        {
            if (!grid.IsInsideGridIndex(coord))
                continue;

            var index = grid.IndexCoordsToArrayIndex(coord);
            board[index].isAvailable = true;
        }
    }
}