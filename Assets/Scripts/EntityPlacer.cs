using UnityEngine;
using static IsometricGrid;


public class EntityPlacer : MonoBehaviour
{
    //private IsometricGrid grid;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("Left mouse button clicked");
            var cameraWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            var entityForPlace = EntityDataManager.Instance.GetEntitiesData()[0]; // Example: Get the first entity data
            var cellIndex = IsometricGrid.Instance.WorldToCellIndex(cameraWorldPosition);
            TryPlaceEntity(cellIndex, entityForPlace);
        }
    }


    public void TryPlaceEntity(Vector2Int position, EntityData entityData)
    {
        if(IsometricGrid.Instance.TryGetCellData(position, out var cellData))
        {
            if(!cellData.isOccupied)
            {
                PlaceEntity(position, cellData, entityData);
            }
            else
            {
                Debug.Log($"Cannot place entity at grid position: {position}, cell is occupied");
            }
        }
        else
        {
            Debug.Log($"Cannot place entity at grid position: {position}, cell by this coordinates does not exist.");
        }

    }


    public void PlaceEntity(Vector2Int position, Cell cellData, EntityData entityData)
    {
        cellData.isOccupied = true;
        var gridPosition = IsometricGrid.Instance.IndexToGridPosition(position);
        TileMapManager.Instance.SetTile(gridPosition, entityData.tile);
    }
}
