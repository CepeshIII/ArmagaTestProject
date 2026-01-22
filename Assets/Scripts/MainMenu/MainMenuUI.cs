using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button quitButton;

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
    }


    private void NewGameButtonPressed() 
    {
        signalBus.TryFire<LoadSceneSignal>( new LoadSceneSignal(1));
    }


    private void ContinueButtonPressed() 
    { 
        signalBus.TryFire<LoadSceneSignal>( new LoadSceneSignal(1));
    }



    private void QuitButtonPressed()
    {
        signalBus.TryFire<QuitFromGameSignal>();
    }

}
