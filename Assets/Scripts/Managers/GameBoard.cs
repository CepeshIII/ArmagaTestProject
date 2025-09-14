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
        var arrayIndex = grid.IndexCoordsToArrayIndex(indexCoords);
        var cell = board[arrayIndex];
        if (CardInstance.cardFactory.TryGetValue(card.CardType, out var creator))
        {
            var cardInstance = creator(card, indexCoords);
            cell.cards.Add(cardInstance);

            if(card is IEffectSourceCard effectSourceCard)
            {
                if(effectSourceCard.GetEffect() != null)
                {
                    SetEffects(effectSourceCard.GetEffect(), indexCoords);
                }
            }
        }
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
