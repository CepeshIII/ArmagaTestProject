using Zenject;


public class PreviewState : IGameState
{

    private readonly SignalBus signalBus;
    private readonly GameStateContext context;



    public PreviewState(GameStateContext context, SignalBus signalBus)
    {
        this.context = context;
        this.signalBus = signalBus;
    }


    public void Enter()
    {
        if(context.UIManager != null)
        {
            context.UIManager.ShowCardInfo();
            context.UIManager.ShowToAttackUI();
        }

        if (context.InputManager != null)
        {
            context.InputManager.ToBoardMode();
        }

        signalBus.Subscribe<MoveIsMadeSignal>(OnMoveIsMade);
    }


    public void Exit()
    {
        if (context.UIManager != null)
        {
            context.UIManager.HideCardInfo();
            context.UIManager.HideToAttackUI();
        }

        if (context.InputManager != null)
        {
            context.InputManager.ToIdle();
        }

        signalBus.Unsubscribe<MoveIsMadeSignal>(OnMoveIsMade);
    }


    private void OnMoveIsMade()
    {
        signalBus.TryFire(new SwitchToNewState(GameState.BattlePhase));
    }
}
