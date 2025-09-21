using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;


public class TileMapManager : MonoBehaviour
{
    [SerializeField] private Tilemap buildingTilemap;
    
    private GameBoard gameBoard;



    [Inject]
    public void Construct(GameBoard gameBoard)
    {
        this.gameBoard = gameBoard;
    }


    private void OnEnable()
    {
        if (gameBoard != null)
        gameBoard.CardPlaced += HandleCardPlaced;
    }


    private void OnDisable()
    {
        if (gameBoard != null)
            gameBoard.CardPlaced -= HandleCardPlaced;
    }


    public void SetTile(Vector2Int position, TileBase tile)
    {
        buildingTilemap.SetTile((Vector3Int)position, tile);
    }


    private void HandleCardPlaced(CardData data, Vector2Int position)
    {
        SetTile(position, data.tile);
    }
}
