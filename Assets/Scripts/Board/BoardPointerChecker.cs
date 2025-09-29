using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;



public class BoardPointerChecker : MonoBehaviour, IInitializable
{
    [SerializeField] private string boardTag = "GameBoard";
    [SerializeField] private GraphicRaycaster graphicRaycaster;

    private InputManager inputManager;
    private IsometricGrid isometricGrid;
    private BoardDisplayer boardDisplayer;

    private Vector2Int lastCoordinate;

    public event Action pointerLeftBoard;
    public event Action<Vector2> newBoardCellOverPointer;




    [Inject]
    public void Construct(InputManager inputManager, IsometricGrid isometricGrid, BoardDisplayer boardDisplayer)
    {
        this.inputManager = inputManager;
        this.isometricGrid = isometricGrid;
        this.boardDisplayer = boardDisplayer;
    }


    public void Initialize()
    {
        inputManager.BoardMouseMoved += HandlerBoardMouseMove;
    }


    private void HandlerBoardMouseMove(Vector2 mousePosition)
    {
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(mousePosition);

        if (IsPointerOverUI(mousePosition)) 
        {
            pointerLeftBoard?.Invoke();
            Debug.Log("Mouse is pointer over UI");
            return;
        }

        if (!IsOverBoardOnly(worldPoint))
        {
            pointerLeftBoard?.Invoke();
            return;
        }

        var coordinate = isometricGrid.WorldToIndexCoords(worldPoint);

        if (!isometricGrid.IsInsideGridIndex(coordinate))
        {
            pointerLeftBoard?.Invoke();
            return;
        }

        if (coordinate != lastCoordinate) 
        {
            newBoardCellOverPointer?.Invoke(coordinate);

            boardDisplayer.SetCellFill(coordinate, true);
            boardDisplayer.SetCellFill(lastCoordinate, false);
        }

        lastCoordinate = coordinate;

        Debug.Log($"Mouse is over Board only: {lastCoordinate}");
    }


    public void CalculateCoordinate()
    {

    }


    bool IsOverBoardOnly(Vector2 worldPoint)
    {
        // Raycast into world
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

        // If nothing hit, return false
        if (hit.collider == null)
            return false;

        // Check if it's the board
        return hit.collider.gameObject.CompareTag(boardTag);
    }


    bool IsPointerOverUI(Vector2 mousePosition)
    {
        var resultList = new List<RaycastResult>();  
        var eventData = new PointerEventData(EventSystem.current);
        eventData.position = mousePosition;

        graphicRaycaster.Raycast(eventData, resultList);

        return resultList.Count > 0;
    }
}
