using System;
using TMPro;
using UnityEngine;


public class CardDeckController : MonoBehaviour
{
    // ----------------- Serialized Fields -----------------

    // ----------------- Private Fields -----------------

    private CardDeck cardDeck;
    private CardDeckDisplay deckView;

    // ----------------- Public Events -----------------


    // ----------------- Public Properties -----------------


    // ----------------- Unity Methods -----------------

    private void OnEnable()
    {
        CardPlacer.Instance.OnCardPlaced += CardPlaced;
        CardPlacer.Instance.OnCardPlacingCanceled += CardPlacingCanceled;

    }


    private void OnDisable()
    {
        if(cardDeck != null)
            cardDeck.OnDeckChanged -= UpdateView;

        if(deckView != null)
            deckView.OnCardChosen -= CardChosen;

        CardPlacer.Instance.OnCardPlaced -= CardPlaced;
        CardPlacer.Instance.OnCardPlacingCanceled -= CardPlacingCanceled;
    }

    // ----------------- Public Methods -----------------

    public void SetCardDeck(CardDeck newCardDeck)
    {
        cardDeck = newCardDeck;
        cardDeck.OnDeckChanged += UpdateView;
        UpdateView();
    }

    public void SetDeckView(CardDeckDisplay newDeckView)
    {
        deckView = newDeckView;
        deckView.OnCardChosen += CardChosen;
        UpdateView();
    }

    // ----------------- Private Methods -----------------

    private void UpdateView()
    {
        if(deckView != null && cardDeck != null)
            deckView.UpdateDisplay(cardDeck.Cards);
    }


    private void CardChosen(CardData cardData)
    {
        CardPlacer.Instance.SetSelectedCard(cardData);
    }


    private void CardPlaced(CardData cardData)
    {
        cardDeck.RemoveCard(cardData);
    }


    private void CardPlacingCanceled(CardData cardData)
    {
        UpdateView();
    }
}
