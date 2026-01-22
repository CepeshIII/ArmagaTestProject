using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


public class UIManager : MonoBehaviour, IUIManager
{
    [SerializeField] GameObject cardInfoUI;
    [SerializeField] GameObject toBoardUI;
    [SerializeField] GameObject toAttackUI;

    [SerializeField] Button openMenuButton;

    private MenuUI menuUI;

    private ICardDeckDisplay deckDisplayer;
    private SignalBus signalBus;

    public event Action ToBoardTriggered;
    public event Action ToAttackTriggered;



    [Inject]
    public void Construct(ICardDeckDisplay deckDisplayer, MenuUI menuUI, SignalBus signalBus)
    {
        this.deckDisplayer = deckDisplayer;
        this.menuUI = menuUI;
        this.signalBus = signalBus;
    }


    private void OnEnable()
    {
        if(openMenuButton != null)
            openMenuButton.onClick.AddListener(ShowMenu);
    }


    private void OnDisable()
    {
        if (openMenuButton != null)
            openMenuButton.onClick.RemoveListener(ShowMenu);
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
        signalBus.TryFire<OpenGamePlayMenu>();
    }


    public void HideMenu()
    {
        signalBus.TryFire<CloseGamePlayMenu>();
    }


    public void HideAll()
    {
        HideDeck();
        HideCardInfo();
        HideToBoardUI();
        HideToAttackUI();
    }
}
