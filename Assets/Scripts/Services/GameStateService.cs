using Zenject;


public class GameStateService
{
    readonly IGameStateManager _manager;


    [Inject]
    public GameStateService(IGameStateManager manager)
    {
        _manager = manager;
    }

}
