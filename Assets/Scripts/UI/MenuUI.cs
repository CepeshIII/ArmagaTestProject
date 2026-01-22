using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


public struct CloseGamePlayMenu { }
public struct OpenGamePlayMenu { }
public struct OpenSettingsSignal { }



public class MenuUI : MonoBehaviour, IInitializable, IDisposable
{
    [SerializeField] private Button returnButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button toMainMenuButton;
    [SerializeField] private Button quitButton;

    private SignalBus signalBus;



    [Inject]
    public void Construct(SignalBus signalBus)
    {
        this.signalBus = signalBus;
    }


    public void Initialize()
    {
        if(returnButton != null)
            returnButton.onClick.AddListener(ReturnButtonWasPressed);
        if(settingsButton != null)
            settingsButton.onClick.AddListener(SettingsButtonWasPressed);
        if(quitButton != null)
            quitButton.onClick.AddListener(QuitButtonWasPressed);
        if(toMainMenuButton != null)
            toMainMenuButton.onClick.AddListener(ToMainMenuButtonWasPressed);

        signalBus.Subscribe<OpenGamePlayMenu>(Show);
        signalBus.Subscribe<CloseGamePlayMenu>(Hide);
    }


    public void Dispose()
    {
        if (returnButton != null)
            returnButton.onClick.RemoveListener(ReturnButtonWasPressed);
        if (settingsButton != null)
            settingsButton.onClick.RemoveListener(SettingsButtonWasPressed);
        if (quitButton != null)
            quitButton.onClick.RemoveListener(QuitButtonWasPressed);
        if (toMainMenuButton != null)
            toMainMenuButton.onClick.AddListener(ToMainMenuButtonWasPressed);

        signalBus.Unsubscribe<OpenGamePlayMenu>(Show);
        signalBus.Unsubscribe<CloseGamePlayMenu>(Hide);
    }
    
    
    public void Show()
    {
        gameObject.SetActive(true);
    }


    public void Hide()
    {
        gameObject.SetActive(false);
    }


    private void ReturnButtonWasPressed()
    {
        signalBus.TryFire<CloseGamePlayMenu>();
    }


    private void SettingsButtonWasPressed()
    {
        signalBus.TryFire<OpenSettingsSignal>();
    }


    private void ToMainMenuButtonWasPressed()
    {
        signalBus.TryFire<ToMainMenuSignal>();
    }


    private void QuitButtonWasPressed()
    {
        signalBus.TryFire<QuitFromGameSignal>();
    }
}
