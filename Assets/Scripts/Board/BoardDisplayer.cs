using System;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;
using Zenject;


public enum QueueElementType
{
    Border,
    Fill
}


public struct QueueElement
{
    public QueueElementType queueElementType;
    public Vector2Int coord;
    public bool value;
}


public class BoardDisplayer : MonoBehaviour, IBoardDisplayer, IDisposable
{
    [SerializeField] private Material boardMaterial;
    [SerializeField] private Vector2Int textureSize = new Vector2Int(16, 16);

    private GameBoard gameBoard;

    private IMaskShaderController maskController;

    private readonly List<QueueElement> dirtyCellsQueue = new();



    [Inject]
    public void Construct(IMaskShaderController shaderController, SignalBus signalBus)
    {
        this.maskController = shaderController;
        signalBus.Subscribe<BoardReadySignal>(x => OnBoardReady(x.Board));
    }


    public void Initialize()
    {
        Debug.Log("BoardDisplayer Initialize");
        maskController.SetMaterial(boardMaterial);
        maskController.CreateAndSetMaskTexture(textureSize);
        maskController.ClearMask();
        maskController.ApplyMask();

        if (gameBoard != null)
        {
            maskController.SetGridOffset(gameBoard.Grid.GridOffset);

            gameBoard.CellAvailabilityChanged += HandleCellAvailabilityChanged;

            DrawBoardBorder();
        }
    }


    private void OnBoardReady(GameBoard board)
    {
        this.gameBoard = board;
        Initialize();
    }


    private void LateUpdate()
    {
        if (dirtyCellsQueue.Count <= 0) return;
        
        foreach(var cell in dirtyCellsQueue)
        {
            switch (cell.queueElementType) 
            { 
                case QueueElementType.Border:
                    maskController.SetBorderPixel(cell.coord, cell.value);
                    break;
                case QueueElementType.Fill:
                    maskController.SetFillPixel(cell.coord, cell.value);
                    break;
            }
        }

        maskController.ApplyMask();
        dirtyCellsQueue.Clear();
    }


    public void Dispose()
    {
        if (gameBoard != null)
        {
            gameBoard.CellAvailabilityChanged -= HandleCellAvailabilityChanged;

        }
    }


    public void SetCellFill(Vector2Int coord, bool isFilled)
    {
        dirtyCellsQueue.Add(new QueueElement
        {
            coord = coord,
            value = isFilled,
            queueElementType = QueueElementType.Fill,
        });
    }


    public void SetCellBorder(Vector2Int coord, bool visible)
    {
        dirtyCellsQueue.Add(new QueueElement
        {
            coord = coord,
            value = visible,
            queueElementType = QueueElementType.Border,
        });
    }


    private void DrawBoardBorder()
    {
        maskController.ClearMask();
        if(gameBoard.BoardCells != null)
        {
            foreach (var cell in gameBoard.BoardCells)
            {
                maskController.SetBorderPixel(cell.indexCoord, cell.isAvailable);
            }
        }

        maskController.ApplyMask();
    }


    private void HandleCellAvailabilityChanged(BoardCellPosition cellPosition, bool isAvailable)
    {
        SetCellBorder(cellPosition.CoordIndex, isAvailable);
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
                var strBuilder = new StringBuilder();
                foreach (var description in card.GetDescription())
                {
                    strBuilder.AppendLine(description);
                }
                var content = new GUIContent(strBuilder.ToString());

                #if UNITY_EDITOR
                    Handles.Label(position, content);
                #endif
            }
        }
    }

}
