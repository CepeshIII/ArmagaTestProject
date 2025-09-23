using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Zenject;


public class BoardDisplayer : MonoBehaviour, IDisposable
{
    [SerializeField] private Material boardMaterial;
    [SerializeField] private Vector2Int textureSize = new Vector2Int(16, 16);

    private GameBoard gameBoard;
    private IMaskShaderController shaderController;

    private readonly List<Tuple<Vector2Int, bool>> dirtCells = new();



    [Inject]
    public void Construct(IMaskShaderController shaderController, SignalBus signalBus)
    {
        this.shaderController = shaderController;
        signalBus.Subscribe<BoardReadySignal>(x => OnBoardReady(x.Board));
    }


    public void Initialize()
    {
        Debug.Log("BoardDisplayer Initialize");
        shaderController.SetMaterial(boardMaterial);
        shaderController.CreateAndSetMaskTexture(textureSize);
        shaderController.ClearMask();
        shaderController.ApplyMask();

        if (gameBoard != null)
        {
            shaderController.SetGridOffset(gameBoard.Grid.GridOffset);

            gameBoard.CellAvailabilityChanged += HandleCellAvailabilityChanged;

            DrawBoard();
        }
    }


    private void OnBoardReady(GameBoard board)
    {
        this.gameBoard = board;

        Initialize();
    }


    private void LateUpdate()
    {
        if (dirtCells.Count > 0)
        {
            foreach (var cell in dirtCells) 
            { 
                shaderController.SetMaskPixel(cell.Item1, cell.Item2);
            }
            shaderController.ApplyMask();
            dirtCells.Clear();
        }
    }


    public void Dispose()
    {
        if (gameBoard != null)
        {
            gameBoard.CellAvailabilityChanged -= HandleCellAvailabilityChanged;

        }
    }


    private void DrawBoard()
    {
        shaderController.ClearMask();
        if(gameBoard.BoardCells != null)
        {
            foreach (var cell in gameBoard.BoardCells)
            {
                shaderController.SetMaskPixel(cell.indexCoord, cell.isAvailable);
            }
        }

        shaderController.ApplyMask();
    }


    private void HandleCellAvailabilityChanged(Vector2Int coord, bool isAvailable)
    {
        dirtCells.Add(new (coord, isAvailable));
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
