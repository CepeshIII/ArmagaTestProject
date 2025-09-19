using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class CardPlacer : Singleton<CardPlacer>
{
    public static event Action<Vector2Int> OnCellSelected;

    public event Action<CardData> CardPlacementConfirmed;
    public event Action<CardData> CardPlacementCanceled;


    private void OnEnable()
    {
        GameBoard.Instance.OnCardPlaced += HandleCardPlaced;
        GameBoard.Instance.OnCardPlacingCanceled += HandleCardPlacingCanceled;
    }


    private void Start()
    {
        // for testing 
        var cardDeck = new CardDeck(new List<CardData>() 
        {
            CardDataBase.Instance.GetCardDataById(0),
            CardDataBase.Instance.GetCardDataById(1),
            CardDataBase.Instance.GetCardDataById(2),
            CardDataBase.Instance.GetCardDataById(3),
            CardDataBase.Instance.GetCardDataById(4),
        });

        var cardDeckController = GameObject.FindAnyObjectByType<CardDeckController>();
        var deckView = GameObject.FindAnyObjectByType<CardDeckDisplay>();

        cardDeckController.SetCardDeck(cardDeck);
        cardDeckController.SetDeckView(deckView);
    }


    private void OnDisable()
    {
        if (GameBoard.Instance != null)
        {
            GameBoard.Instance.OnCardPlaced -= HandleCardPlaced;
            GameBoard.Instance.OnCardPlacingCanceled -= HandleCardPlacingCanceled;
        }
    }


    public void TryPlaceCard(CardData card, Vector3 worldPosition)
    {
        GameBoard.Instance.TryPlaceCardAtWorldPosition(card, worldPosition);
    }


    private void HandleCardPlaced(CardData card, Vector2Int indexCoords)
    {
        CardPlacementConfirmed.Invoke(card);
    }


    private void HandleCardPlacingCanceled(CardData card, Vector2Int indexCoords)
    {
        CardPlacementCanceled.Invoke(card);
    }


}
