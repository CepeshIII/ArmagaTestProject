using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;


public struct QuitFromGameSignal { }
public struct ToMainMenuSignal { }


public struct LoadSceneSignal
{

    public int Index { get; private set; }


    public LoadSceneSignal(int index)
    {
        Index = index;
    }
}



public class SceneLoader : MonoBehaviour, IInitializable, IDisposable
{
    private SignalBus signalBus;


    [Inject]
    public void Construct(SignalBus signalBus)
    {
        this.signalBus = signalBus;
    }


    public void Initialize()
    {
        signalBus.Subscribe<LoadSceneSignal>(LoadScene);
        signalBus.Subscribe<QuitFromGameSignal>(Quit);
        signalBus.Subscribe<ToMainMenuSignal>(LoadMainMenuScene);
    }


    public void Dispose()
    {
        signalBus.Unsubscribe<LoadSceneSignal>(LoadScene);
        signalBus.Unsubscribe<QuitFromGameSignal>(Quit);
        signalBus.Unsubscribe<ToMainMenuSignal>(LoadMainMenuScene);
    }


    private void LoadScene(LoadSceneSignal signal)
    {
        if(signal.Index < 0 || signal.Index >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogError($"Scene index {signal.Index} is out of bounds.");
            return;
        }

        SceneManager.LoadScene(signal.Index);
    }


    private void LoadMainMenuScene()
    {
        SceneManager.LoadScene(0);
    }


    private void Quit()
    {
        Application.Quit();
    }
}
