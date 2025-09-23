using UnityEngine;
using Zenject;

public class BoardService : IBoardService
{
    private readonly BoardCellsBuilder cellsBuilder;
    private readonly GameBoard gameBoard;
    private readonly SignalBus signalBus;


    public BoardService(BoardCellsBuilder cellsBuilder, GameBoard gameBoard, SignalBus signalBus)
    {
        this.cellsBuilder = cellsBuilder;
        this.gameBoard = gameBoard;
        this.signalBus = signalBus;
    }


    public void SetupBoard()
    {
        // Placement rules
        var placementValidator = PlacementRulesBuilder.CreateDefault();

        // Build board cells
        var boardCells = cellsBuilder.CreateCells();
        cellsBuilder.SetAvailableCells(boardCells, new Vector2Int[]
        {
            new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(2, 0),
            new Vector2Int(0, 1), new Vector2Int(1, 1), new Vector2Int(2, 1),
            new Vector2Int(0, 2), new Vector2Int(1, 2), new Vector2Int(2, 2),
        });

        // Set PlacementRules
        gameBoard.SetPlacementValidator(placementValidator);

        // Create GameBoard
        gameBoard.SetBoardCells(boardCells);

        // Invoke signal BoardReadySignal
        signalBus.TryFire(new BoardReadySignal(gameBoard));
    }
}