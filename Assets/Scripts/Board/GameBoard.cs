using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


// Class representing a board cell
public class Cell
{
    public List<CardInstance> cards;      // Cards currently on this cell
    public List<EffectInstance> effects;      // Effects applied to this cell
    public Vector2Int indexCoord;         // Position of the cell on the board (x, y)
    public bool isAvailable;              // Whether the cell can be occupied or used
}


/// <summary>
/// Zenject signal used to notify that a GameBoard is fully created, initialized
/// </summary>
public class BoardReadySignal
{
    public GameBoard Board { get; private set; }

    public BoardReadySignal(GameBoard board)
    {
        Board = board;
    }
}




public struct BoardCellPosition
{
    public readonly Vector3 WorldPosition;
    public readonly Vector2Int GridPosition;
    public readonly Vector2Int CoordIndex;

    public BoardCellPosition(Vector3 world, Vector2Int grid, Vector2Int index)
    {
        WorldPosition = world;
        GridPosition = grid;
        CoordIndex = index;
    }
}


public class GameBoard
{
    private ILinearGrid grid;
    private PlacementValidator placementValidator;

    private EffectFactory effectFactory;
    private CardInstanceFactory cardFactory;

    private Cell[] boardCells;

    public Cell[] BoardCells { get => boardCells; }
    public ILinearGrid Grid { get => grid; }


    public event Action<BoardCellPosition, bool> CellAvailabilityChanged;
    public event Action<CardInstance, BoardCellPosition> CardPlaced;
    public event Action<CardData, BoardCellPosition> CardPlacingCanceled;
    public event Action BoardUpdated;



    [Inject]
    public GameBoard(ILinearGrid grid, EffectFactory effectFactory, CardInstanceFactory cardFactory)
    {
        this.grid = grid;
        this.effectFactory = effectFactory;
        this.cardFactory = cardFactory;
    }


    public void SetPlacementValidator(PlacementValidator placementValidator)
    {
        this.placementValidator = placementValidator;
    }


    public void SetBoardCells(Cell[] newBoard)
    {
        boardCells = newBoard;
        Debug.Log("SetBoardCells");
    }


    public void TryPlaceCardAtWorldPosition(CardData card, Vector3 worldPosition)
    {
        var gridPosition = grid.WorldToGridPosition(worldPosition);
        var indexCoords = grid.GridPositionToIndexCoords(gridPosition);
        TryPlaceCard(card, indexCoords);
    }


    public void TryPlaceCard(CardData card, Vector2Int indexCoords)
    {
        var worldPosition = grid.IndexCoordsToWorldCorner(indexCoords);
        var gridPosition = grid.IndexCoordsToGridPosition(indexCoords);
        var boardCellPosition = new BoardCellPosition(worldPosition, gridPosition, indexCoords);

        if (TryGetCell(indexCoords, out var cell) && placementValidator.CanPlace(cell, card))
        {
            var cardInstance = CreateCardInstance(card, indexCoords, cell);
            CardPlaced?.Invoke(cardInstance, boardCellPosition);
            RecalculateEffects();
            BoardUpdated.Invoke();
        }
        else
        {
            CardPlacingCanceled?.Invoke(card, boardCellPosition);
        }
    }


    public bool TryGetCell(Vector2Int indexCoords, out Cell cell)
    {
        if (grid.IsInsideGridIndex(indexCoords))
        {
            var index = grid.IndexCoordsToArrayIndex(indexCoords);
            cell = boardCells[index];
            return true;
        }
        cell = null;
        return false;
    }


    public bool TryGetCellAtWorldPosition(Vector3 worldPosition, out Cell cell)
    {
        var gridPosition = grid.WorldToGridPosition(worldPosition);
        var indexCoords = grid.GridPositionToIndexCoords(gridPosition);

        return TryGetCell(indexCoords, out cell);
    }


    public bool TryGetIndexCoordsAtWorldPosition(Vector3 worldPosition, out Vector2Int indexCoords)
    {
        var gridPosition = grid.WorldToGridPosition(worldPosition);
        indexCoords = grid.GridPositionToIndexCoords(gridPosition);

        if (grid.IsInsideGridIndex(indexCoords))
        {
            return true;
        }

        return false;
    }


    public bool CellIsAvailable(Vector2Int indexCoords)
    {
        var cell = GetCell(indexCoords);
        if (cell != null)
        {
            return cell.isAvailable;
        }
        return false;
    }


    public Cell GetCell(Vector2Int indexCoords)
    {
        if (grid.IsInsideGridIndex(indexCoords))
        {
            var index = grid.IndexCoordsToArrayIndex(indexCoords);
            return boardCells[index];
        }

        return null;
    }


    /// <summary>
    /// Creates a CardInstance, assigns it to a cell on the board, 
    /// and registers any effects for later application.
    /// </summary>
    /// <param displayName="cardData">The sourceCard data to instantiate.</param>
    /// <param displayName="indexCoords">The coordinates of the target cell on the board.</param>
    /// <param displayName="cell">The cell where the CardInstance will be placed.</param>
    private CardInstance CreateCardInstance(CardData cardData, Vector2Int indexCoords, Cell cell)
    {
        // Create a new card instance from the factory
        if (cardFactory.TryGetInstance(cardData, out var cardInstance))
        {
            // Assign the board position to the instance
            cardInstance.Move(indexCoords);

            // Add the instance to the target cell
            cell.cards.Add(cardInstance);

            // Register effects if the card is an effect source
            if (cardData is IEffectSourceCard effectSourceCard)
            {
                RegisterEffectsFromCard(effectSourceCard, indexCoords);
            }

            return cardInstance;
        }

        return null;
    }


    /// <summary>
    /// Retrieves all effects from the given effect source sourceCard and registers them
    /// to the specified cell for later application by the board's effect system.
    /// </summary>
    /// <param displayName="sourceCard">The sourceCard that provides effects.</param>
    /// <param displayName="indexCoords">The coordinate of cell to which the effects will be added.</param>
    private void RegisterEffectsFromCard(IEffectSourceCard sourceCard, Vector2Int indexCoords)
    {
        var effects = sourceCard.GetEffects();
        if (effects == null) return;

        foreach (var effect in effects)
        {
            if (effect != null)
            {
                var effectInstance = new EffectInstance(effect, sourceCard);
                SetEffectInstances(effectInstance, indexCoords);
            }
        }
    }


    /// <summary>
    /// Adds the specified effect to all cells within its effect area relative to the given origin coordinates.
    /// </summary>
    /// <param displayName="effect">The effect data to be applied.</param>
    /// <param displayName="originCoords">The origin coordinates from which the effect area is calculated.</param>
    private void SetEffectInstances(EffectInstance instance, Vector2Int originCoords)
    {
        // Get all cell coordinates affected by this effect, based on its effect area and board size
        foreach (var effectedCellCoords in
            EffectAreaCalculator.GetPositions(instance.Data.effectArea, originCoords, grid.GridSize))
        {
            SetEffect(instance, effectedCellCoords);
        }
    }


    /// <summary>
    /// Adds the specified effect to cell
    /// </summary>
    private void SetEffect(EffectInstance instance, Vector2Int indexCoords)
    {
        // Check if the cell coordinates are inside the board grid
        if (grid.IsInsideGridIndex(indexCoords))
        {
            // ConvertIn 2D cell coordinates to 1D array index
            var index = grid.IndexCoordsToArrayIndex(indexCoords);

            // Get the cell from the board array
            var effectedCell = boardCells[index];

            // Register the effect to the cell for future application
            effectedCell.effects.Add(instance);
        }
    }


    private void RecalculateEffects()
    {
        EffectCollector();
        ResetParameters();
        ApplyEffects();
    }


    /// <summary>
    /// Cleans up all effects on the board by removing those
    /// whose source is no longer valid (i.e., null).
    /// </summary>
    private void EffectCollector()
    {
        foreach (var cell in boardCells)
        {
            cell.effects.RemoveAll(effectInstance => effectInstance.Source == null);
        }
    }


    /// <summary>
    /// Resets all parameters of cards on the board that were modified by effects.
    /// Call this method after all card placements and effect applications are completed
    /// to prepare the board for new calculations and effect applications.
    /// </summary>
    private void ResetParameters()
    {
        foreach (var cell in boardCells)
        {
            foreach (var card in cell.cards)
            {
                card.ResetParam();
            }
        }
    }


    /// <summary>
    /// Applies all registered effects on the board to the cards in their respective cells.
    /// Call this method after all parameters have been reset and cards have been placed,
    /// so that new calculations and effect applications are performed correctly.
    /// </summary>
    private void ApplyEffects()
    {
        // Iterate over all cells on the board
        foreach (var cell in boardCells)
        {
            // Iterate over all effects registered in this cell
            foreach (var effectInstance in cell.effects)
            {
                // Get the concrete effect implementation from the factory
                var effect = effectFactory.GetEffect(effectInstance.Data);
                if (effect == null)
                    continue;

                // Apply the effect to each card present in the cell
                foreach (var card in cell.cards)
                {
                    effect.Apply(card, effectInstance.Data.effectValue);
                }
            }
        }
    }

}
