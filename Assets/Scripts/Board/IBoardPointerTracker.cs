using System;
using static BoardPointerTracker;


public interface IBoardPointerTracker
{
    public event Action<PointerEventArgs> PointerExitedBoard;
    public event Action<PointerEventArgs> PointerEnteredBoardCell;
    public event Action<PointerEventArgs> PointerMovedToNewCell;
}