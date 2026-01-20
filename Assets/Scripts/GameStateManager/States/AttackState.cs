using Zenject;


public struct RoundWinSignal {}
public struct RoundLooseSignal { }


public class AttackState : IGameState
{

    private readonly SignalBus signalBus;
    private readonly GameStateContext context;

    private static readonly int stateID = (int)GameState.BattlePhase;


    public AttackState(GameStateContext context, SignalBus signalBus)
    {
        this.context = context;
        this.signalBus = signalBus;
    }


    public void Enter()
    {
        if(context.UIManager != null)
        {
            context.UIManager.ShowToBoardUI();
        }

        if (context.InputManager != null)
        {
            context.InputManager.ToGameMode();
        }

        signalBus.Subscribe<RoundWinSignal>(OnRoundWin);
        signalBus.Subscribe<RoundLooseSignal>(OnRoundLose);
    }


    public void Exit()
    {
        if (context.UIManager != null)
        {
            context.UIManager.HideToBoardUI();
        }

        if (context.InputManager != null)
        {
            context.InputManager.ToIdle();
        }

        signalBus.Unsubscribe<RoundWinSignal>(OnRoundWin);
        signalBus.Unsubscribe<RoundLooseSignal>(OnRoundLose);
    }


    public void OnRoundWin()
    {
        signalBus.TryFire(new SwitchToNewState(GameState.RoundPassed));
    }


    public void OnRoundLose()
    {
        signalBus.TryFire(new SwitchToNewState(GameState.GameOver));
    }


    public int GetID()
    {
        return (int)stateID;
    }
}
