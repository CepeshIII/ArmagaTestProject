using System;
using UnityEngine;
using Zenject;


public struct PlacementCompletedSignal { }



public class CardDeckController : MonoBehaviour, IInitializable, IDisposable
{
    private CardDeck cardDeck;
    private CardDeckDisplay deckDisplay;
    private CardPlacer cardPlacer;

    private SignalBus signalBus;



    [Inject]
    public void Construct(CardDeckDisplay deckDisplay, CardPlacer cardPlacer, SignalBus signalBus)
    {
        this.cardPlacer = cardPlacer;
        this.deckDisplay = deckDisplay;
        this.signalBus = signalBus;
    }


    public void Initialize()
    {
        if (deckDisplay != null)
        {
            deckDisplay.CardDropped += HandleCardDropped;
        }

        if (cardPlacer != null)
        {
            cardPlacer.CardPlacementConfirmed += HandleCardPlaced;
            cardPlacer.CardPlacementCanceled += HandleCardPlacementCanceled;
        }

    }


    public void Dispose()
    {
        if (cardDeck != null)
            cardDeck.DeckChanged -= UpdateView;

        if (deckDisplay != null)
            deckDisplay.CardDropped -= HandleCardDropped;

        if (cardPlacer != null)
        {
            cardPlacer.CardPlacementConfirmed -= HandleCardPlaced;
            cardPlacer.CardPlacementCanceled -= HandleCardPlacementCanceled;
        }
    }
    

    public void SetDeck(CardDeck deck)
    {
        cardDeck = deck;

        if (cardDeck != null)
        {
            UpdateView();
            cardDeck.DeckChanged += UpdateView;
        }
    }


    public CardDeck GetDeck()
    {
        return cardDeck;
    }


    private void UpdateView()
    {
        if(deckDisplay != null && cardDeck != null)
            deckDisplay.UpdateDisplay(cardDeck.Cards);
    }


    private void HandleCardDropped(CardData cardData, Vector3 position)
    {
        if(cardPlacer != null)
            cardPlacer.TryPlaceCard(cardData, position);
    }


    private void HandleCardPlaced(CardData cardData)
    {
        if (cardPlacer != null)
        {
            cardDeck.RemoveCard(cardData);

            // In future, should be building a more complex system of control how many card can be placed, for now just fire after each placement
            signalBus.Fire<PlacementCompletedSignal>();
        }

    }


    private void HandleCardPlacementCanceled(CardData cardData)
    {
        UpdateView();
    }

}
