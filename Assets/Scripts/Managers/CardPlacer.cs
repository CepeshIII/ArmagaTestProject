using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class CardPlacer : MonoBehaviour, IInitializable, IDisposable
{
    public static event Action<Vector2Int> OnCellSelected;

    private GameBoard gameBoard;

    public event Action<CardData> CardPlacementConfirmed;
    public event Action<CardData> CardPlacementCanceled;



    [Inject]
    public void Construct(GameBoard gameBoard)
    {
        this.gameBoard = gameBoard;

    }


    public void Initialize()
    {
        if (gameBoard != null)
        {
            gameBoard.OnCardPlaced += HandleCardPlaced;
            gameBoard.OnCardPlacingCanceled += HandleCardPlacingCanceled;
        }
    }


    public void Dispose()
    {
        if (gameBoard != null)
        {
            gameBoard.OnCardPlaced -= HandleCardPlaced;
            gameBoard.OnCardPlacingCanceled -= HandleCardPlacingCanceled;
        }
    }


    public void TryPlaceCard(CardData card, Vector3 worldPosition)
    {
        if (gameBoard != null)
        {
            gameBoard.TryPlaceCardAtWorldPosition(card, worldPosition);
        }
    }


    private void HandleCardPlaced(CardData card, Vector2Int gridPosition)
    {
        CardPlacementConfirmed.Invoke(card);
    }


    private void HandleCardPlacingCanceled(CardData card, Vector2Int gridPosition)
    {
        CardPlacementCanceled.Invoke(card);
    }

}
