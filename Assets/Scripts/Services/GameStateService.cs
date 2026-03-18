using Zenject;


public class GameStateService : IGameStateService
{
    readonly IGameStateManager _manager;
    readonly SignalBus signalBus;



    [Inject]
    public GameStateService(IGameStateManager manager, SignalBus signalBus)
    {
        _manager = manager;
        this.signalBus = signalBus;
    }


    public void SetupGameStateMachine()
    {
        signalBus.TryFire(new SwitchToNewState(GameState.CardPlacement));
    }
}
