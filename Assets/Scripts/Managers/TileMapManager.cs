using UnityEngine;
using UnityEngine.Tilemaps;


public class TileMapManager : Singleton<TileMapManager>
{
    [SerializeField] private Tilemap buildingTilemap;


    new public void Awake()
    {
        base.Awake();
    }


    public void SetTile(Vector2Int position, TileBase tile)
    {
        buildingTilemap.SetTile((Vector3Int)position, tile);
    }

}
