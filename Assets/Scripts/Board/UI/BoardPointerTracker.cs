using System;
using UnityEngine;
using Zenject;



/// <summary>
/// Represents possible states of the pointer when interacting with the game board.
/// </summary>
public enum BoardPointerStates
{
    ExitBoard,   // Pointer is outside the board
    EnterBoard,  // Pointer just entered the board
    StayOnBoard, // Pointer remains on the same board cell
    MoveOnBoard, // Pointer moved to a different board cell
}



/// <summary>
/// Tracks the mouse pointer position relative to the game board
/// and raises events when pointer state changes occur.
/// </summary>
public class BoardPointerTracker : MonoBehaviour, IBoardPointerTracker, IInitializable, IDisposable
{
    /// <summary>
    /// Internal data structure storing pointer information.
    /// </summary>
    private struct PointerData
    {
        public Vector2 displayPosition; // Position in screen/display space
        public Vector3 worldPosition;   // Position in world space
        public Vector2Int coordinate;   // Board cell coordinates
    }

    private MainUIGraphicRaycaster graphicRaycaster;
    private InputManager inputManager;
    private GameBoard gameBoard;
    private new Camera camera;

    private TagHandle boardTag;
    private Vector2Int lastCoordinate = new(-1, -1);
    private BoardPointerStates currentState;

    // Events for external subscribers
    public event Action<PointerEventArgs> PointerExitedBoard;
    public event Action<PointerEventArgs> PointerEnteredBoardCell;
    public event Action<PointerEventArgs> PointerMovedToNewCell;

    /// <summary>
    /// Event arguments for pointer-related events, containing last and new board cell coordinates.
    /// </summary>
    public class PointerEventArgs : EventArgs
    {
        public Vector2Int LastCoord { get; }
        public Vector2Int NewCoord { get; }

        public PointerEventArgs(Vector2Int lastCoord, Vector2Int newCoord)
        {
            LastCoord = lastCoord;
            NewCoord = newCoord;
        }
    }



    /// <summary>
    /// Injects required dependencies.
    /// </summary>
    [Inject]
    public void Construct(InputManager inputManager,
        GameBoard gameBoard, Camera camera, MainUIGraphicRaycaster graphicRaycaster)
    {
        this.inputManager = inputManager;
        this.gameBoard = gameBoard;
        this.camera = camera;
        this.graphicRaycaster = graphicRaycaster;
    }


    /// <summary>
    /// Initializes the tracker, subscribing to input events.
    /// </summary>
    public void Initialize()
    {
        boardTag = TagHandle.GetExistingTag("GameBoard");
        if (inputManager != null)
            inputManager.BoardMouseMoved += HandleBoardMouseMove;
    }


    /// <summary>
    /// Cleans up by unsubscribing from events.
    /// </summary>
    public void Dispose()
    {
        if (inputManager != null && camera != null)
            inputManager.BoardMouseMoved -= HandleBoardMouseMove;
    }


    /// <summary>
    /// Handles pointer movement on the board.
    /// </summary>
    private void HandleBoardMouseMove(Vector2 mousePosition)
    {
        var pointerData = CalculatePointerData(mousePosition);
        var nextState = CalculateState(pointerData);

        ProcessState(pointerData, nextState);

        lastCoordinate = pointerData.coordinate;
        currentState = nextState;
    }


    /// <summary>
    /// Converts a pointer display position into world and board coordinate data.
    /// </summary>
    private PointerData CalculatePointerData(Vector2 pointerDisplayPosition)
    {
        Vector2 worldPoint = camera.ScreenToWorldPoint(pointerDisplayPosition);
        gameBoard.TryGetIndexCoordsAtWorldPosition(worldPoint, out var coordinate);

        return new PointerData
        {
            displayPosition = pointerDisplayPosition,
            worldPosition = worldPoint,
            coordinate = coordinate
        };
    }


    /// <summary>
    /// Determines the next pointer state based on position and board conditions.
    /// </summary>
    private BoardPointerStates CalculateState(PointerData pointerData)
    {
        var nextState = currentState;

        // If pointer is not valid for board interaction, treat as exit
        if (graphicRaycaster.IsPointerOverUI(pointerData.displayPosition) ||
            !IsOverBoardOnly(pointerData.worldPosition) ||
            !gameBoard.CellIsAvailable(pointerData.coordinate))
        {
            nextState = BoardPointerStates.ExitBoard;
        }
        else if (currentState == BoardPointerStates.ExitBoard)
        {
            nextState = BoardPointerStates.EnterBoard;
        }
        else if (pointerData.coordinate != lastCoordinate)
        {
            nextState = BoardPointerStates.MoveOnBoard;
        }
        else
        {
            nextState = BoardPointerStates.StayOnBoard;
        }

        return nextState;
    }


    /// <summary>
    /// Processes the currentSettings pointer state and raises appropriate events.
    /// </summary>
    private void ProcessState(PointerData pointerData, BoardPointerStates nextState)
    {
        switch (nextState)
        {
            case BoardPointerStates.ExitBoard:
                PointerExitedBoard?.Invoke(
                    new PointerEventArgs(lastCoordinate, pointerData.coordinate));
                lastCoordinate = new Vector2Int(-1, -1); // Reset last coordinate
                break;

            case BoardPointerStates.EnterBoard:
                PointerEnteredBoardCell?.Invoke(
                    new PointerEventArgs(lastCoordinate, pointerData.coordinate));
                break;

            case BoardPointerStates.MoveOnBoard:
                PointerMovedToNewCell?.Invoke(
                    new PointerEventArgs(lastCoordinate, pointerData.coordinate));
                break;
        }
    }


    /// <summary>
    /// Checks whether the pointer is only over the board collider.
    /// </summary>
    private bool IsOverBoardOnly(Vector2 worldPoint)
    {
        var collider = Physics2D.OverlapPoint(worldPoint);

        if (collider == null)
            return false;

        return collider.gameObject.CompareTag(boardTag);
    }

}
