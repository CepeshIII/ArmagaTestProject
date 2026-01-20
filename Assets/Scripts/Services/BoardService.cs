using System;
using UnityEngine;
using Zenject;



public struct CardPlacedSignal { }



public class BoardService : IBoardService, IInitializable, IDisposable
{
    private readonly BoardCellsBuilder cellsBuilder;
    private readonly GameBoard gameBoard;
    private readonly SignalBus signalBus;

    private Cell[] boardCells;


    [Inject]
    public BoardService(BoardCellsBuilder cellsBuilder, GameBoard gameBoard, SignalBus signalBus)
    {
        this.cellsBuilder = cellsBuilder;
        this.gameBoard = gameBoard;
        this.signalBus = signalBus;
    }


    public void Initialize()
    {
        gameBoard.CardPlaced += OnCardPlaced;
    }


    private void OnCardPlaced(CardInstance instance, BoardCellPosition boardCellPosition)
    {
        signalBus.TryFire(new CardPlacedSignal());
    }


    public void Dispose()
    {
        throw new NotImplementedException();
    }


    public GameBoard GetBoard()
    {
        return gameBoard;
    }


    public void SetupBoard()
    {
        // Placement rules
        var placementValidator = PlacementRulesBuilder.CreateDefault();

        // Build board cells
        boardCells = cellsBuilder.CreateCells();
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
