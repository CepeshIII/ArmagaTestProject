
using System;
using Zenject;
using static BoardPointerTracker;


public class BoardHighlighter : IInitializable, IDisposable
{
    private readonly BoardPointerTracker boardPointerTracker;
    private readonly BoardDisplayer boardDisplayer;



    public BoardHighlighter(BoardDisplayer boardDisplayer,
        BoardPointerTracker boardPointerTracker)
    {
        this.boardDisplayer = boardDisplayer;
        this.boardPointerTracker = boardPointerTracker;
    }


    public void Initialize()
    {
        if (boardPointerTracker == null) return;

        boardPointerTracker.PointerEnteredBoardCell += HandlePointerEnteredBoardCell;
        boardPointerTracker.PointerExitedBoard += HandlePointerExitedBoardCell;
        boardPointerTracker.PointerMovedToNewCell += HandlePointerMovedToNewCell;
    }


    public void Dispose()
    {
        if (boardPointerTracker == null) return;

        boardPointerTracker.PointerEnteredBoardCell -= HandlePointerEnteredBoardCell;
        boardPointerTracker.PointerExitedBoard -= HandlePointerExitedBoardCell;
        boardPointerTracker.PointerMovedToNewCell -= HandlePointerMovedToNewCell;
    }


    private void HandlePointerEnteredBoardCell(PointerEventArgs args)
    {
        boardDisplayer.SetCellFill(args.NewCoord, true);
    }


    private void HandlePointerMovedToNewCell(PointerEventArgs args)
    {
        boardDisplayer.SetCellFill(args.NewCoord, true);
        boardDisplayer.SetCellFill(args.LastCoord, false);
    }


    private void HandlePointerExitedBoardCell(PointerEventArgs args)
    {
        boardDisplayer.SetCellFill(args.LastCoord, false);
    }

}