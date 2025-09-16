using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class CardPlacer : Singleton<CardPlacer>
{
    private CardData selectedCard;

    public static event Action<Vector2Int> OnCellSelected;

    public event Action<CardData> OnCardPlaced;
    public event Action<CardData> OnCardPlacingCanceled;


    private void OnEnable()
    {
        InputManager.Instance.OnBoardClicked_End += LeftMouseClick;
        GameBoard.Instance.OnCardPlaced += CardPlaced;
        GameBoard.Instance.OnCardPlacingCanceled += CardPlacingCanceled;
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
        if(InputManager.Instance != null)
            InputManager.Instance.OnBoardClicked_Start -= LeftMouseClick;

        GameBoard.Instance.OnCardPlaced -= CardPlaced;
        GameBoard.Instance.OnCardPlacingCanceled -= CardPlacingCanceled;
    }


    public void SetSelectedCard(CardData cardData)
    {
        selectedCard = cardData;
    }


    private void LeftMouseClick(Vector3 mousePosition)
    {
        // if no card selected to place return
        if (selectedCard == null)
        {
            return;
        }

        SetCard(selectedCard, mousePosition);
    }


    private void SetCard(CardData card, Vector3 worldPosition)
    {
        var gridPosition = IsometricGrid.Instance.WorldToGridPosition(worldPosition);
        var indexCoords = IsometricGrid.Instance.GridPositionToIndexCoords(gridPosition);
        GameBoard.Instance.SetCard(card, indexCoords);
    }


    private void CardPlaced(CardData card, Vector2Int indexCoords)
    {
        var gridPosition = IsometricGrid.Instance.IndexCoordsToGridPosition(indexCoords);
        TileMapManager.Instance.SetTile(gridPosition, card.tile);

        OnCardPlaced.Invoke(selectedCard);
        selectedCard = null;
    }


    private void CardPlacingCanceled(CardData card, Vector2Int indexCoords)
    {
        OnCardPlacingCanceled.Invoke(selectedCard);
        selectedCard = null;
    }


}
