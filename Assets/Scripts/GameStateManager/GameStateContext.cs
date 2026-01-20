public class GameStateContext
{
    private readonly InputManager inputManager;
    private readonly IUIManager uiManager;

    public InputManager InputManager { get => inputManager; }
    public IUIManager UIManager { get => uiManager; }



    public GameStateContext(InputManager inputManager, IUIManager uiManager)
    {
        this.inputManager = inputManager;
        this.uiManager = uiManager;
    }
}
