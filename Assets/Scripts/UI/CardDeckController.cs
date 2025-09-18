using UnityEngine;


public class CardDeckController : MonoBehaviour
{
    private CardDeck cardDeck;
    private CardDeckDisplay deckView;


    private void OnEnable()
    {
        if (cardDeck != null)
            cardDeck.DeckChanged += UpdateView;

        if (deckView != null)
            deckView.CardDropped += HandleCardDropped;

        CardPlacer.Instance.CardPlacementConfirmed += HandleCardPlaced;
        CardPlacer.Instance.CardPlacementCanceled += HandleCardPlacementCanceled;
    }


    private void OnDisable()
    {
        if(cardDeck != null)
            cardDeck.DeckChanged -= UpdateView;

        if(deckView != null)
            deckView.CardDropped -= HandleCardDropped;

        if (CardPlacer.Instance != null)
        {
            CardPlacer.Instance.CardPlacementConfirmed -= HandleCardPlaced;
            CardPlacer.Instance.CardPlacementCanceled -= HandleCardPlacementCanceled;
        }

    }

    
    public void SetCardDeck(CardDeck newCardDeck)
    {
        if (cardDeck != null)
        {
            cardDeck.DeckChanged -= UpdateView;
        }

        if (newCardDeck != null)
        {   
            cardDeck = newCardDeck;
            cardDeck.DeckChanged += UpdateView;
        }

        UpdateView();
    }


    public void SetDeckView(CardDeckDisplay newDeckView)
    {
        if (deckView != null)
        {
            deckView.CardDropped -= HandleCardDropped;
        }

        if (newDeckView != null)
        {
            deckView = newDeckView;
            deckView.CardDropped += HandleCardDropped;
        }

        UpdateView();
    }


    private void UpdateView()
    {
        if(deckView != null && cardDeck != null)
            deckView.UpdateDisplay(cardDeck.Cards);
    }


    private void HandleCardDropped(CardData cardData, Vector3 position)
    {
        CardPlacer.Instance.TryPlaceCard(cardData, position);
    }


    private void HandleCardPlaced(CardData cardData)
    {
        cardDeck.RemoveCard(cardData);
    }


    private void HandleCardPlacementCanceled(CardData cardData)
    {
        UpdateView();
    }
}
