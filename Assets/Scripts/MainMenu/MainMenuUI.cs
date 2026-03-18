using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


public struct OpenSettingsMenuSignal { }
public struct CloseSettingsMenuSignal { }


public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button settingsButton;


    private SignalBus signalBus;


    [Inject]
    public void Construct(SignalBus signalBus)
    {
        this.signalBus = signalBus;
    }


    public void OnEnable()
    {
        if (newGameButton != null)
        {
            newGameButton.onClick.AddListener(NewGameButtonPressed);
        }

        if (continueButton != null)
        {
            continueButton.onClick.AddListener(ContinueButtonPressed);
        }

        if (quitButton != null)
        {
            quitButton.onClick.AddListener(QuitButtonPressed);
        }

        if (settingsButton != null)
            settingsButton.onClick.AddListener(SettingsButtonWasPressed);
    }


    public void OnDisable()
    {
        if (newGameButton != null)
        {
            newGameButton.onClick.RemoveListener(NewGameButtonPressed);
        }

        if (continueButton != null)
        {
            continueButton.onClick.RemoveListener(ContinueButtonPressed);
        }

        if (quitButton != null)
        {
            quitButton.onClick.RemoveListener(QuitButtonPressed);
        }

        if (settingsButton != null)
            settingsButton.onClick.RemoveListener(SettingsButtonWasPressed);
    }


    private void NewGameButtonPressed() 
    {
        signalBus.TryFire<LoadSceneSignal>( new LoadSceneSignal(1));
    }


    private void ContinueButtonPressed() 
    { 
        signalBus.TryFire<LoadSceneSignal>( new LoadSceneSignal(1));
    }


    private void ReturnButtonWasPressed()
    {
        signalBus.TryFire<CloseGamePlayMenu>();
    }


    private void SettingsButtonWasPressed()
    {
        signalBus.TryFire<OpenSettingsMenuSignal>();
    }



    private void QuitButtonPressed()
    {
        signalBus.TryFire<QuitFromGameSignal>();
    }

}
