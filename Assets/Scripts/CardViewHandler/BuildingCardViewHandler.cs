using System.Linq;
using UnityEngine;


public class BuildingCardViewHandler : ICardViewHandler
{
    private readonly CardPrefabFactory cardPrefabFactory;
    private readonly ICellPlacementStrategy placementStrategy;

    private GameObject prefabObject;

    public CardInstance Instance { get; private set; }
    public BoardCellPosition Position { get; private set; }



    public BuildingCardViewHandler(CardPrefabFactory cardPrefabFactory, ICellPlacementStrategy placementStrategy)
    {
        this.cardPrefabFactory = cardPrefabFactory;
        this.placementStrategy = placementStrategy;
    }


    public void CreateView(CardInstance instance, Transform parent)
    {
        Instance = instance;

        if (cardPrefabFactory.TryGetGameObject(instance.Data, out var gameObject))
        {
            gameObject.transform.position = placementStrategy.GetPositions(instance.IndexCoords, 1).First();

            prefabObject = gameObject;
        }
    }


    public void RemoveView()
    {
        GameObject.Destroy(prefabObject);
    }


    public void UpdateView()
    {
        prefabObject.transform.position = placementStrategy.GetPositions(Instance.IndexCoords, 1).First();
    }
}
