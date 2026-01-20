using Zenject;



public class CardPlacementState : IGameState
{

    private readonly SignalBus signalBus;
    private readonly GameStateContext context;



    public CardPlacementState(GameStateContext context, SignalBus signalBus)
    {
        this.context = context;
        this.signalBus = signalBus;
    }


    public void Enter()
    {
        if (context.UIManager != null)
        {
            context.UIManager.ShowDeck();
            context.UIManager.ShowCardInfo();
            context.UIManager.ShowToAttackUI();
        }

        if (context.InputManager != null)
        {
            context.InputManager.ToBoardMode();
        }

        signalBus.Subscribe<CardPlacedSignal>(OnCardPlaced);
        signalBus.Subscribe<MoveIsMadeSignal>(OnMoveIsMade);
        signalBus.Subscribe<PlacementCompletedSignal>(PlacementCompleted);
    }


    public void Exit()
    {
        if (context.UIManager != null)
        {
            context.UIManager.HideDeck();
            context.UIManager.HideCardInfo();
            context.UIManager.HideToAttackUI();
        }

        if (context.InputManager != null)
        {
            context.InputManager.ToIdle();
        }

        signalBus.Unsubscribe<CardPlacedSignal>(OnCardPlaced);
        signalBus.Unsubscribe<MoveIsMadeSignal>(OnMoveIsMade);
        signalBus.Unsubscribe<PlacementCompletedSignal>(PlacementCompleted);
    }


    private void OnCardPlaced()
    {
    }


    private void PlacementCompleted()
    {
        signalBus.TryFire(new SwitchToNewState(GameState.PreviewPhase));
    }


    private void OnMoveIsMade()
    {
        signalBus.TryFire(new SwitchToNewState(GameState.BattlePhase));
    }
}
