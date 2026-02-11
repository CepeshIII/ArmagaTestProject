using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameOverMenuUI : MonoBehaviour, IInitializable, IDisposable
{
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
        if (quitButton != null)
            quitButton.onClick.AddListener(QuitButtonWasPressed);
        if (toMainMenuButton != null)
            toMainMenuButton.onClick.AddListener(ToMainMenuButtonWasPressed);
    }


    public void Dispose()
    {
        if (quitButton != null)
            quitButton.onClick.RemoveListener(QuitButtonWasPressed);
        if (toMainMenuButton != null)
            toMainMenuButton.onClick.AddListener(ToMainMenuButtonWasPressed);
    }


    public void Show()
    {
        gameObject.SetActive(true);
    }


    public void Hide()
    {
        gameObject.SetActive(false);
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
