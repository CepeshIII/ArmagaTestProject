using UnityEngine;
using Zenject;

public class RoundManager: IInitializable
{
    private readonly GameBoard gameBoard;
    private readonly BoardCellsBuilder boardCellsBuilder;
    private readonly CardDeckBuilder deckBuilder;
    private readonly CardDeckController deckController;

    private readonly DiContainer container;



    [Inject]
    public RoundManager(GameBoard gameBoard, BoardCellsBuilder boardCellsBuilder,
        CardDeckBuilder deckBuilder, CardDeckController deckController, DiContainer container)
    {
        this.gameBoard = gameBoard;
        this.boardCellsBuilder = boardCellsBuilder;
        this.deckBuilder = deckBuilder;
        this.deckController = deckController;

        this.container = container;
    }


    public void Initialize()
    {
        StartNewRound();
    }


    public void StartNewRound()
    {
        SetupBoard();
        var deck = CreateDeck();
    }


    private void SetupBoard()
    {
        // Placement rules
        var placementValidator = PlacementRulesBuilder.CreateDefault();

        // Build board cells
        var boardCells = boardCellsBuilder.CreateCells();
        boardCellsBuilder.SetAvailableCells(boardCells, new Vector2Int[]
        {
            new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(2, 0),
            new Vector2Int(0, 1), new Vector2Int(1, 1), new Vector2Int(2, 1),
            new Vector2Int(0, 2), new Vector2Int(1, 2), new Vector2Int(2, 2),
        });

        // Set PlacementRules
        gameBoard.SetPlacementValidator(placementValidator);

        // Create GameBoard
        gameBoard.SetBoardCells(boardCells);

        // Bind this instance so other classes can inject it
        //container.Bind<GameBoard>().FromInstance(gameBoard).AsSingle();
    }


    private CardDeck CreateDeck()
    {
        //Create a fresh deck
        var deck = deckBuilder.CreateRandomDeck(5);

        //Bind it for this round
        RoundInstaller.Install(container, deck);

        deckController.SetDeck(deck);

        return deck;
    }

}
