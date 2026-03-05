using Zenject;


public struct RoundBeginSignal { }
public struct RoundWinSignal {}
public struct RoundLooseSignal { }


public class AttackState : IGameState
{

    private readonly SignalBus signalBus;
    private readonly GameStateContext context;
    private readonly BattleRoundController roundController;
    private readonly BattleFlowController flowController;

    private static readonly int stateID = (int)GameState.BattlePhase;


    public AttackState(BattleRoundController roundController, 
        GameStateContext context, BattleFlowController flowController, SignalBus signalBus)
    {
        this.context = context;
        this.signalBus = signalBus;
        this.roundController = roundController;
        this.flowController = flowController;
    }


    public void Enter()
    {
        context.UIManager?.ShowToBoardUI();
        context.InputManager?.ToGameMode();

        roundController.StartNextRound();
        flowController.StartBattle();

        signalBus.TryFire<RoundBeginSignal>();
        signalBus.Subscribe<RoundWinSignal>(OnRoundWin);
        signalBus.Subscribe<RoundLooseSignal>(OnRoundLose);

        flowController.OnBattleFinished += OnBattleFinished;
    }


    public void Exit()
    {
        context.UIManager?.HideToBoardUI();
        context.InputManager?.ToIdle();

        signalBus.TryUnsubscribe<RoundWinSignal>(OnRoundWin);
        signalBus.TryUnsubscribe<RoundLooseSignal>(OnRoundLose);

        flowController.OnBattleFinished -= OnBattleFinished;
    }


    public void OnRoundWin()
    {
        if (roundController.AllRoundsPassed)
        {
            signalBus.TryFire(new SwitchToNewState(GameState.LevelPassed));
        }
        else
        {
            signalBus.TryFire(new SwitchToNewState(GameState.RoundPassed));
        }
    }


    public void OnRoundLose()
    {
        signalBus.TryFire(new SwitchToNewState(GameState.GameOver));
    }


    public int GetID()
    {
        return (int)stateID;
    }


    private void OnBattleFinished(Team winner)
    {
        if (winner == Team.Player)
            signalBus.TryFire<RoundWinSignal>();
        else
            signalBus.TryFire<RoundLooseSignal>();
    }
}
