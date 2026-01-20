using System;
using Zenject;


public struct MoveIsMadeSignal { }


public class RoundManager: IInitializable, IDisposable
{
    private readonly IGridService gridService;
    private readonly IBoardService boardService;
    private readonly IDeckService deckService;
    private readonly UIManager uiManager;
    private readonly SignalBus signalBus;

    private RoundStats currentRoundStats;

    public RoundStats CurrentRoundStats => currentRoundStats;

    public event Action PlacementPhaseCompleted;
    public event Action PreviewPhaseCompleted;
    public event Action BattlePhaseCompleted;
    public event Action GameOver;



    public RoundManager(IGridService gridService, IBoardService boardService, 
        IDeckService deckService, UIManager uiManager, SignalBus signalBus)
    {
        this.gridService = gridService;
        this.boardService = boardService;
        this.deckService = deckService;
        this.uiManager = uiManager;
        this.signalBus = signalBus;
    }


    public void Initialize()
    {
        if (uiManager != null)
        {
            uiManager.ToAttackTriggered += OnPreviewPhaseEnded;
            uiManager.ToBoardTriggered += OnBattlePhaseEnded;
        }

        //signalBus.Subscribe<CardPlacedSignal>(OnCardPlaced);
    }


    public void Dispose()
    {
        var board = boardService.GetBoard();

        if (uiManager != null)
        {
            uiManager.ToAttackTriggered -= OnPreviewPhaseEnded;
            uiManager.ToBoardTriggered -= OnBattlePhaseEnded;
        }
        //signalBus.Unsubscribe<CardPlacedSignal>(OnCardPlaced);
    }


    public void SetStats(RoundStats stats)
    {
        currentRoundStats = stats;
    }


    public void InitRound()
    {
        deckService.CreateAndAssignDeck();
        gridService.BuildGrid();
        boardService.SetupBoard();
    }


    public void StartNewRound()
    {
        deckService.AddCardToDeck();
    }


    //private void OnCardPlaced()
    //{
    //    currentRoundStats.placedCardsCount++;

    //    // handle logic for placement
    //    if (AllCardsPlaced())
    //    {
    //        signalBus.TryFire<MoveIsMadeSignal>();
    //        //PlacementPhaseCompleted?.Invoke();
    //    }
    //}


    private void OnPreviewPhaseEnded()
    {
        PreviewPhaseCompleted?.Invoke();
    }


    private void OnBattlePhaseEnded()
    {
        BattlePhaseCompleted?.Invoke();
    }


    public bool CanPlaceMoreCards()
    {
        return currentRoundStats.maxPlacedCardsCount < currentRoundStats.placedCardsCount;
    }

}
