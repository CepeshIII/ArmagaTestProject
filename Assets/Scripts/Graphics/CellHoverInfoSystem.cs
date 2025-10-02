using System;
using UnityEngine;
using Zenject;
using static BoardPointerTracker;


public class CellHoverInfoSystem : IInitializable, ITickable, IDisposable
{
    private bool isPointerOverBoard;
    private bool isLastCellWasDisplayed;

    private  Vector2Int lastCoordinate;
    private float hoverDelay = 0.5f;
    private float timer;

    private readonly BoardPointerTracker boardPointerChecker;
    private readonly GameBoard gameBoard;
    private readonly ICellInfoWindow infoWindow;


    public CellHoverInfoSystem(BoardPointerTracker boardPointerChecker, GameBoard gameBoard, ICellInfoWindow infoWindow)
    {
        this.boardPointerChecker = boardPointerChecker;
        this.gameBoard = gameBoard;
        this.infoWindow = infoWindow;
    }


    public void Initialize()
    {
        boardPointerChecker.PointerExitedBoard += BoardPointerChecker_PointerExitedBoard;
        boardPointerChecker.PointerEnteredBoardCell += BoardPointerChecker_PointerMovedToNewCell;
        boardPointerChecker.PointerMovedToNewCell += BoardPointerChecker_PointerMovedToNewCell;
    }


    public void Tick()
    {
        if (!isPointerOverBoard || isLastCellWasDisplayed) return;

        if(timer > 0)
        {
            timer -= Time.deltaTime;
            return;
        }

        var cell = gameBoard.GetCell(lastCoordinate);
        if (cell != null && infoWindow != null)
            infoWindow.Display(cell);
        isLastCellWasDisplayed = true;
    }


    public void Dispose()
    {
        if (boardPointerChecker != null)
        {
            boardPointerChecker.PointerExitedBoard -= BoardPointerChecker_PointerExitedBoard;
            boardPointerChecker.PointerEnteredBoardCell -= BoardPointerChecker_PointerMovedToNewCell;
            boardPointerChecker.PointerMovedToNewCell -= BoardPointerChecker_PointerMovedToNewCell;
        }
    }


    public void SetHoverDelay(float hoverDelay)
    {
        this.hoverDelay = hoverDelay;
    }


    private void BoardPointerChecker_PointerMovedToNewCell(PointerEventArgs args)
    {
        isPointerOverBoard = true;
        isLastCellWasDisplayed = false;
        infoWindow.Hide();
        lastCoordinate = args.NewCoord;
        ResetTimer();
    }


    private void BoardPointerChecker_PointerExitedBoard(PointerEventArgs args)
    {
        isPointerOverBoard = false;
        isLastCellWasDisplayed = false;
        infoWindow.Hide();
    }


    private void ResetTimer()
    {
        timer = hoverDelay;
    }
}
