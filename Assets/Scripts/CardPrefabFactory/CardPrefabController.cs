using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;



public class CardPrefabEntry
{
    public CardInstance Instance { get; private set; }
    public GameObject PrefabObject { get; private set; }
    public BoardCellPosition Position { get; private set; }



    public CardPrefabEntry(CardInstance instance, GameObject prefabObject, BoardCellPosition position)
    {
        Instance = instance;
        PrefabObject = prefabObject;
        Position = position;
    }
}



public class CardPrefabController: IInitializable, IDisposable
{
    private readonly CardPrefabFactory cardPrefabFactory;
    private readonly CardViewHandlerFactory cardViewHandlerFactory;
    private readonly GameBoard gameBoard;

    private Dictionary<Vector2Int, ICardViewHandler> viewHandlers;


    [Inject]
    public CardPrefabController(GameBoard gameBoard, CardPrefabFactory cardPrefabFactory, 
        CardViewHandlerFactory cardViewHandlerFactory)
    {
        this.gameBoard = gameBoard;
        this.cardPrefabFactory = cardPrefabFactory;
        this.cardViewHandlerFactory = cardViewHandlerFactory;
    }


    public void Initialize()
    {
        Debug.Log("CardPrefabController Initialize");
        if (gameBoard != null)
        {
            gameBoard.CardPlaced += HandleCardPlaced;
            gameBoard.BoardUpdated += HandleGameBoardUpdated;
        }

        viewHandlers = new();
    }


    public void Dispose()
    {
        if (gameBoard != null) 
        {
            gameBoard.CardPlaced -= HandleCardPlaced;
        }
    }


    private void HandleCardPlaced(CardInstance cardInstance, BoardCellPosition cellPosition)
    {
        Debug.Log($"HandleCardPlaced. Info: \ncoordinate: {cellPosition.CoordIndex};\ngridPosition: {cellPosition.GridPosition};\nWorldPosition: {cellPosition.WorldPosition}");
        var viewHandler = cardViewHandlerFactory.GetHandler(cardInstance.Data.CardType, cardPrefabFactory, gameBoard.Grid);

        if (viewHandler != null)
        {
            viewHandler.CreateView(cardInstance, null);
            viewHandlers.Add(cellPosition.CoordIndex, viewHandler);
        }
    }


    private void HandleGameBoardUpdated()
    {
        foreach(var viewHandler in viewHandlers.Values)
        {
            viewHandler.UpdateView();
        }
    }


    private void HandleCardRemoved(CardInstance cardInstance, BoardCellPosition cellPosition)
    {
        if (viewHandlers.TryGetValue(cellPosition.CoordIndex, out var viewHandler))
        {
            viewHandler?.RemoveView();
            viewHandlers.Remove(cellPosition.CoordIndex);
        }

    }


}
