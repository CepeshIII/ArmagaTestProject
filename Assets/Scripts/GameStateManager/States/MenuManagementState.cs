using Zenject;

public class MenuManagementState : IGameState
{

    private readonly SignalBus signalBus;
    private readonly GameStateContext context;


    public MenuManagementState(SignalBus signalBus, GameStateContext context)
    {
        this.signalBus = signalBus;
        this.context = context;
    }


    public void Enter()
    {
        if (context.UIManager != null)
        {
            context.UIManager.ShowMenu();
        }

        if (context.InputManager != null)
        {
            context.InputManager.ToIdle();
        }
    }


    public void Exit()
    {
        if (context.UIManager != null)
        {
            context.UIManager.HideMenu();
        }
    }

}
