public class GameStateContext
{
    private readonly InputManager inputManager;
    private readonly UIManager uiManager;

    public InputManager InputManager { get => inputManager; }
    public UIManager UIManager { get => uiManager; }



    public GameStateContext(InputManager inputManager, UIManager uiManager)
    {
        this.inputManager = inputManager;
        this.uiManager = uiManager;
    }
}
