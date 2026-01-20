using UnityEngine;

public class RoundContext
{
    private readonly RoundManager roundManager;
    private readonly IGridService gridService;
    private readonly IBoardService boardService;
    private readonly IDeckService deckService;  
    


    private RoundContext(RoundManager roundManager, IGridService gridService, 
        IBoardService boardService, IDeckService deckService)
    {
        this.roundManager = roundManager;
        this.gridService = gridService;
        this.boardService = boardService;
        this.deckService = deckService;
    }
}
