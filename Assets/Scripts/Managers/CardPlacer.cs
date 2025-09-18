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
        });

        var cardDeckController = GameObject.FindAnyObjectByType<CardDeckController>();
        var deckView = GameObject.FindAnyObjectByType<CardDeckDisplay>();

        cardDeckController.SetCardDeck(cardDeck);
        cardDeckController.SetDeckView(deckView);

        // for testing purposes, set a default selected card
        //selectedCard = CardDataBase.Instance.GetCardDataById(0); // Example: Get the first card data
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
        var gridPosition = IsometricGrid.Instance.WorldToGridPosition(worldPosition);
        var indexCoords = IsometricGrid.Instance.GridPositionToIndexCoords(gridPosition);
        GameBoard.Instance.TryPlaceCard(card, indexCoords);
    }


    private void HandleCardPlaced(CardData card, Vector2Int indexCoords)
    {
        var gridPosition = IsometricGrid.Instance.IndexCoordsToGridPosition(indexCoords);
        TileMapManager.Instance.SetTile(gridPosition, card.tile);

        CardPlacementConfirmed.Invoke(card);
    }


    private void HandleCardPlacingCanceled(CardData card, Vector2Int indexCoords)
    {
        CardPlacementCanceled.Invoke(card);
    }


}
