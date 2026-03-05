using Zenject;

public class LevelPassedState : IGameState
{
    private readonly SignalBus signalBus;
    private readonly GameStateContext context;


    public LevelPassedState(GameStateContext context, SignalBus signalBus)
    {
        this.context = context;
        this.signalBus = signalBus;
    }


    public void Enter()
    {
        context.UIManager.ShowGameOverMenu();
    }


    public void Exit()
    {

    }

}

