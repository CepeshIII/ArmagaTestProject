using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class GameBoard : Singleton<GameBoard>
{
    public class Cell
    {
        public List<CardInstance> cards;
        public List<EffectData> effects;
        public Vector2Int indexCoord;
        public bool isAvailable;
    }

    private IsometricGrid grid;
    private Cell[] board;
    private PlacementValidator placementValidator;


    public Cell[] Board { get => board; }
    public IsometricGrid Grid { get => grid; }


    // Temporary for debug
    public void Start()
    {
        SetGrid(IsometricGrid.Instance);
        SetCellsAvailable(new Vector2Int[] 
        { 
            new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(2, 0),
            new Vector2Int(0, 1), new Vector2Int(1, 1), new Vector2Int(2, 1),
            new Vector2Int(0, 2), new Vector2Int(1, 2), new Vector2Int(2, 2),
        });

        placementValidator = new PlacementValidator(this);

        placementValidator.AddMandatoryRule(new PlacementRules.CellIsNotNullRule());
        placementValidator.AddMandatoryRule(new PlacementRules.CellAvailableRule());

        placementValidator.AddOptionalRule(new PlacementRules.CellEmptyRule());
        placementValidator.AddOptionalRule(new PlacementRules.SameCardRule());
    }


    public void SetGrid(IsometricGrid newGrid)
    {
        grid = newGrid;
        InitializeBoard(grid);
    }


    public void SetCellsAvailable(Vector2Int[] cellIndexCoord)
    {
        foreach(var indexCoord in cellIndexCoord)
        {
            if(!grid.IsInsideGridIndex(indexCoord))
            {
                continue;
            }
            var index = grid.IndexCoordsToArrayIndex(indexCoord);
            board[index].isAvailable = true;
        }
    }


    public void SetCard(CardData card, Vector2Int indexCoords)
    {
        if(placementValidator.CanPlace(indexCoords, card))
        {
            var arrayIndex = grid.IndexCoordsToArrayIndex(indexCoords);
            var cell = board[arrayIndex];
            CreateCardInstance(card, indexCoords, cell);
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
            cell = board[index];
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
            return board[index];
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
                var effectedCell = board[index];
                effectedCell.effects.Add(effect);
            }
        }
    }

    // Call this method after all placements are done for resetting effects values
    private void ResetParameters()
    {
        foreach (var cell in board)
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
        foreach (var cell in board)
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


    private void InitializeBoard(IsometricGrid grid)
    {
        board = new Cell[grid.GridSize.x * grid.GridSize.y];

        var index = 0;
        for (int y = 0; y < grid.GridSize.y; y++)
        {
            for (int x = 0; x < grid.GridSize.x; x++)
            {
                board[index] = new Cell 
                { 
                    cards = new(),
                    effects = new(),
                    isAvailable = false,
                    indexCoord = new Vector2Int(x, y)
                };
                index++;
            }
        }
    }


    private void OnDrawGizmos()
    {
        if (board == null) return;

        foreach (var cell in board)
        {
            var indexCoord = cell.indexCoord;
            var position = IsometricGrid.Instance.IndexCoordsToWorldCenter(indexCoord);

            foreach (var card in cell.cards)
            {
                var content = new GUIContent(card.GetDescription());
                Handles.Label(position, content);
            }
        }
    }
}
