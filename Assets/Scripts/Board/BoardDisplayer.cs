using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Zenject;


public class BoardDisplayer : MonoBehaviour, IInitializable, IDisposable
{
    [SerializeField] private Material boardMaterial;
    [SerializeField] private Vector2Int textureSize = new Vector2Int(16, 16);

    private GameBoard gameBoard;
    private GridShaderController gridShaderController;

    private readonly List<Tuple<Vector2Int, bool>> dirtCells = new();


    [Inject]
    public void Construct(GameBoard gameBoard)
    {
        this.gameBoard = gameBoard;
    }


    public void Initialize()
    {
        gridShaderController = new GridShaderController();
        gridShaderController.SetMaterial(boardMaterial);
        gridShaderController.CreateAndSetMaskTexture(textureSize);
        gridShaderController.ClearMask();
        gridShaderController.ApplyMask();

        if (gameBoard != null)
        {
            gridShaderController.SetGridOffset(gameBoard.Grid.GridOffset);

            gameBoard.BoardCellsChange += HandleBoardCellsChange;
            gameBoard.CellAvailabilityChanged += HandleCellAvailabilityChanged;
        }
    }


    private void LateUpdate()
    {
        if (dirtCells.Count > 0)
        {
            foreach (var cell in dirtCells) 
            { 
                gridShaderController.SetMaskPixel(cell.Item1, cell.Item2);
            }
            gridShaderController.ApplyMask();
            dirtCells.Clear();
        }
    }


    public void Dispose()
    {
        if (gameBoard != null)
        {
            gameBoard.BoardCellsChange -= HandleBoardCellsChange;
            gameBoard.CellAvailabilityChanged -= HandleCellAvailabilityChanged;

        }
    }


    private void DrawBoard()
    {
        gridShaderController.ClearMask();

        foreach (var cell in gameBoard.BoardCells)
        {
            gridShaderController.SetMaskPixel(cell.indexCoord, cell.isAvailable);
        }

        gridShaderController.ApplyMask();
    }


    private void HandleCellAvailabilityChanged(Vector2Int coord, bool isAvailable)
    {
        dirtCells.Add(new (coord, isAvailable));
    }


    private void HandleBoardCellsChange()
    {
        DrawBoard();
    }


    private void OnDrawGizmos()
    {
        if (gameBoard == null || gameBoard.BoardCells == null) return;

        foreach (var cell in gameBoard.BoardCells)
        {
            var indexCoord = cell.indexCoord;
            var position = gameBoard.Grid.IndexCoordsToWorldCenter(indexCoord);

            foreach (var card in cell.cards)
            {
                var content = new GUIContent(card.GetDescription());
                Handles.Label(position, content);
            }
        }
    }

}
