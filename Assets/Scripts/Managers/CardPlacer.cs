using UnityEngine;
using static IsometricGrid;


public class CardPlacer : MonoBehaviour
{
    //private IsometricGrid grid;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("Left mouse button clicked");
            var cameraWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            var cardForPlace = CardDataBase.Instance.GetCardDataById(0); // Example: Get the first card data
            var gridPosition = IsometricGrid.Instance.WorldToGridPosition(cameraWorldPosition);
            

            var indexCoords = IsometricGrid.Instance.GridPositionToIndexCoords(gridPosition);
            GameBoard.Instance.SetCard(cardForPlace, indexCoords);
        }
    }

}
