using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class Cell
{
    public List<CardInstance> cards;
    public List<EffectData> effects;
    public Vector2Int indexCoord;
    public bool isAvailable;
}


public class GameBoard
{
    private IsometricGrid grid;
    private PlacementValidator placementValidator;

    private Cell[] boardCells;

    public Cell[] BoardCells { get => boardCells; }
    public IsometricGrid Grid { get => grid; }


    public event Action BoardCellsChange;
    public event Action<Vector2Int, bool> CellAvailabilityChanged;
    public event Action<CardData, Vector2Int> CardPlaced;
    public event Action<CardData, Vector2Int> CardPlacingCanceled;



    public GameBoard(IsometricGrid grid)
    {
        this.grid = grid;
    }


    public void SetPlacementValidator(PlacementValidator placementValidator)
    {
        this.placementValidator = placementValidator;
    }


    public void SetBoardCells(Cell[] newBoard)
    {
        boardCells = newBoard;
        BoardCellsChange?.Invoke();
    }


    public void TryPlaceCardAtWorldPosition(CardData card, Vector3 worldPosition)
    {
        var gridPosition = grid.WorldToGridPosition(worldPosition);
        var indexCoords = grid.GridPositionToIndexCoords(gridPosition);
        TryPlaceCard(card, indexCoords);
    }


    public void TryPlaceCard(CardData card, Vector2Int indexCoords)
    {
        var gridPosition = grid.IndexCoordsToGridPosition(indexCoords);
        if (TryGetCell(indexCoords, out var cell) && placementValidator.CanPlace(cell, card))
        {
            CreateCardInstance(card, indexCoords, cell);
            CardPlaced?.Invoke(card, gridPosition);
        }
        else
        {
            CardPlacingCanceled?.Invoke(card, gridPosition);
        }
    }


    private void CreateCardInstance(CardData card, Vector2Int indexCoords, Cell cell)
    {
        // Create card instance using factory method by card type
        if (CardInstance.cardFactory.TryGetValue(card.CardType, out var creator))
        {
            var cardInstance = creator(card, indexCoords);
            cell.cards.Add(cardInstance);

            // Get and apply effects if the card is an effect source
            if (card is IEffectSourceCard effectSourceCard)
            {
                if (effectSourceCard.GetEffect() != null)
                {
                    SetEffects(effectSourceCard.GetEffect(), indexCoords);
                }
            }
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


    public Cell GetCell(Vector2Int indexCoords)
    {
        if (grid.IsInsideGridIndex(indexCoords))
        {
            var index = grid.IndexCoordsToArrayIndex(indexCoords);
            return boardCells[index];
        }

        Debug.LogWarning("IndexCoords out of grid size");

        return null;
    }


    private void SetEffects(EffectData effect, Vector2Int indexCoords)
    {
        foreach (var effectedCellIndexCoords in EffectsAreaParser.ParseEffectArea(effect.area, indexCoords, grid.GridSize))
        {
            if (grid.IsInsideGridIndex(effectedCellIndexCoords))
            {
                var index = grid.IndexCoordsToArrayIndex(effectedCellIndexCoords);
                var effectedCell = boardCells[index];
                effectedCell.effects.Add(effect);
            }
        }
    }

    // Call this method after all placements are done for resetting effects values
    private void ResetParameters()
    {
        foreach (var cell in boardCells)
        {
            foreach(var card in cell.cards)
            {
                card.ResetParam();
            }
        }
    }


    // Call this methods after all parameters are reset for calculation new values
    private void ApplyEffects()
    {
        foreach (var cell in boardCells)
        {
            foreach (var effect in cell.effects)
            {
                foreach(var card in cell.cards)
                {
                    effect.ApplyEffect(card);
                }
            }
        }
    }

}
