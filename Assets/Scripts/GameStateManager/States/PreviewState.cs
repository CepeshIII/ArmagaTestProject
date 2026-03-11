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

        signalBus.TryUnsubscribe<MoveIsMadeSignal>(OnMoveIsMade);
    }


    private void OnMoveIsMade()
    {
        signalBus.TryFire(new SwitchToNewState(GameState.BattlePhase));
    }
}



public class RoundPassedState : IGameState
{

    private readonly SignalBus signalBus;
    private readonly GameStateContext context;
    private readonly BattleSceneConfig battleSceneConfig;


    public RoundPassedState(GameStateContext context, 
        BattleSceneConfig battleSceneConfig, SignalBus signalBus)
    {
        this.context = context;
        this.battleSceneConfig = battleSceneConfig;
        this.signalBus = signalBus;
    }


    public void Enter()
    {

        if (context.UIManager != null)
        {
            context.UIManager.UpdateRoundsLine(battleSceneConfig);
        }

        if (context.InputManager != null)
        {
        }

        signalBus.TryFire(new SwitchToNewState(GameState.CardPlacement));
    }


    public void Exit()
    {
        if (context.UIManager != null)
        {
        }

        if (context.InputManager != null)
        {
        }
    }

}
