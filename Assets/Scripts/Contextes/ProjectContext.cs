
using Zenject;

public class ProjectContext
{
    private readonly SignalBus signalBus;

    // Add other services and managers as needed
    //private readonly AudioManager audioManager;
    //private readonly SaveSystem saveSystem;
    //private readonly SceneLoader sceneLoader;
    //private readonly DataBase configDatabase;
    
    
    
    public ProjectContext(SignalBus signalBus)
    {
        this.signalBus = signalBus;
    }
}
