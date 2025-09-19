using System;
using UnityEngine;
using UnityEngine.Tilemaps;


public class TileMapManager : Singleton<TileMapManager>
{
    [SerializeField] private Tilemap buildingTilemap;
    [SerializeField] private GameBoard gameBoard;



    new public void Awake()
    {
        base.Awake();
    }


    private void OnEnable()
    {
        gameBoard = GameBoard.Instance;
        if (gameBoard != null)
        gameBoard.OnCardPlaced += HandleCardPlaced;
    }


    private void OnDisable()
    {
        if (gameBoard != null)
            gameBoard.OnCardPlaced -= HandleCardPlaced;
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
