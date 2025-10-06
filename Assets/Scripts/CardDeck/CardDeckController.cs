using System;
using UnityEngine;
using Zenject;


public class CardDeckController : MonoBehaviour, IInitializable, IDisposable
{
    private CardDeck cardDeck;
    private CardDeckDisplay deckDisplay;
    private CardPlacer cardPlacer;



    [Inject]
    public void Construct(CardDeckDisplay deckDisplay, CardPlacer cardPlacer)
    {
        this.cardPlacer = cardPlacer;
        this.deckDisplay = deckDisplay;
    }


    public void Initialize()
    {
        if (deckDisplay != null)
            deckDisplay.CardDropped += HandleCardDropped;

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
            cardDeck.RemoveCard(cardData);
    }


    private void HandleCardPlacementCanceled(CardData cardData)
    {
        UpdateView();
    }

}
