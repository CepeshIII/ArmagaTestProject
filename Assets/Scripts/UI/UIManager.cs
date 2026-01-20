using System;
using UnityEngine;
using Zenject;


public class UIManager : MonoBehaviour, IUIManager
{
    [SerializeField] GameObject cardInfoUI;

    [SerializeField] GameObject toBoardUI;
    [SerializeField] GameObject toAttackUI;

    [SerializeField] MenuUI menuUI;

    private ICardDeckDisplay deckDisplayer;
    private SignalBus signalBus;

    public event Action ToBoardTriggered;
    public event Action ToAttackTriggered;



    [Inject]
    public void Construct(ICardDeckDisplay deckDisplayer, SignalBus signalBus)
    {
        this.signalBus = signalBus;
        this.deckDisplayer = deckDisplayer;
    }


    public void ShowDeck()
    {
        if (deckDisplayer != null)
        {
            deckDisplayer.ShowCardDisplayers();
        }
    }


    public void HideDeck()
    {
        if(deckDisplayer != null)
        {
            deckDisplayer.Hide();
        }
    }


    public void ShowCardInfo()
    {
        if (deckDisplayer != null)
        {
            cardInfoUI.SetActive(true);
        }
    }


    public void HideCardInfo()
    {
        if (deckDisplayer != null)
        {
            cardInfoUI.SetActive(false);
        }
    }


    public void ShowToBoardUI()
    {
        if (toBoardUI != null)
        {
            toBoardUI.SetActive(true);
        }
    }


    public void HideToBoardUI()
    {
        if (toBoardUI != null)
        {
            toBoardUI.SetActive(false);
        }
    }


    public void ShowToAttackUI()
    {
        if (toAttackUI != null)
        {
            toAttackUI.SetActive(true);
        }
    }


    public void HideToAttackUI()
    {
        if (toAttackUI != null)
        {
            toAttackUI.SetActive(false);
        }
    }


    public void OnToBoardButtonPressed()
    {
        signalBus.TryFire(new SwitchToNewState(GameState.CardPlacement));

        //signalBus.TryFire(new MoveIsMadeSignal());
        //ToBoardTriggered?.Invoke();
    }


    public void OnToAttackButtonPressed()
    {
        signalBus.TryFire(new SwitchToNewState(GameState.BattlePhase));
        //ToAttackTriggered?.Invoke();
    }


    public void ShowMenu()
    {
        if (menuUI != null)
        {
            menuUI.Show();
        }
    }


    public void HideMenu()
    {
        if (menuUI != null)
        {
            menuUI.Hide();
        }
    }


    public void HideAll()
    {
        HideDeck();
        HideCardInfo();
        HideToBoardUI();
        HideToAttackUI();
    }
}
