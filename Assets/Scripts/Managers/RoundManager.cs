using UnityEngine;
using Zenject;

public class RoundManager: IInitializable
{
    private readonly IGridService gridService;
    private readonly IBoardService boardService;
    private readonly IDeckService deckService;



    [Inject]
    public RoundManager(IGridService gridService, IBoardService boardService, IDeckService deckService)
    {
        this.gridService = gridService;
        this.boardService = boardService;
        this.deckService = deckService;
    }


    public void Initialize()
    {
        Debug.Log("RoundManager Initialize");
        StartNewRound();
    }


    public void StartNewRound()
    {
        gridService.BuildGrid();
        boardService.SetupBoard();
        deckService.CreateAndAssignDeck();
    }

}
