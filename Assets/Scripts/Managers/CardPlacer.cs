using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class CardPlacer : MonoBehaviour
{
    public static event Action<Vector2Int> OnCellSelected;

    private CardData selectedCard;


    public void OnEnable()
    {
        InputManager.Instance.OnBoardClick += _ => LeftMouseClick();
        InputManager.Instance.OnBoardClick += _ => RightMouseClick();
    }


    private void LeftMouseClick()
    {
        var mouseCameraPosition = InputManager.Instance.GetMousePosition();
        var mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseCameraPosition);
        var cardForPlace = CardDataBase.Instance.GetCardDataById(0); // Example: Get the first card data
        SetCard(mouseWorldPosition, cardForPlace);
    }


    private void RightMouseClick()
    {

    }


    private void SetCard(Vector3 worldPosition, CardData card)
    {
        var gridPosition = IsometricGrid.Instance.WorldToGridPosition(worldPosition);
        var indexCoords = IsometricGrid.Instance.GridPositionToIndexCoords(gridPosition);
        GameBoard.Instance.SetCard(card, indexCoords);
    }


    public void OnDisable()
    {
        InputManager.actions.BoardManageMode.LeftMouseClick.started -= _ => LeftMouseClick();
        InputManager.actions.BoardManageMode.RightMouseClick.started -= _ => RightMouseClick();
    }
}
