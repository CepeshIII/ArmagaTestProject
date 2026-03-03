using UnityEngine;


public class BattleEntityBuildingCardViewHandler : ICardViewHandler
{
    private BattleEntity viewObject;

    private readonly UnitsManager battleEntityManager;
    private readonly ICellPlacementStrategy placementStrategy;
    private readonly BoardEntityRegistry boardEntityRegistry;

    public CardInstance Instance { get; private set; }
    public BattleEntityDefinition definition { get; private set; }
    public BoardCellPosition Position { get; private set; }



    public BattleEntityBuildingCardViewHandler(UnitsManager unitManager,
        ICellPlacementStrategy placementStrategy, BoardEntityRegistry boardEntityRegistry)
    {
        this.battleEntityManager = unitManager;
        this.placementStrategy = placementStrategy;
        this.boardEntityRegistry = boardEntityRegistry;
    }


    public void CreateView(CardInstance instance, Transform parent)
    {
        if (instance is BuildingCardInstance buildingInstance)
        {
            Instance = instance;
            definition = buildingInstance.entityDefinition;
            RemoveView();
        }
    }


    public void RemoveView()
    {
        if (viewObject != null) 
        { 
            battleEntityManager.DeactivateEntity(viewObject);
            boardEntityRegistry.Unregister(viewObject);
        }
    }


    public void UpdateView()
    {
        if (Instance is not BuildingCardInstance buildingInstance)
            return;

        RemoveView();

        var positions = placementStrategy.GetPositions(
            Instance.IndexCoords, 1);

        viewObject = battleEntityManager.GetFreeEntity(definition);

        var position = positions[0];
        boardEntityRegistry.Register(viewObject, Position, position);

        viewObject.SetTeam(Team.Player);
        viewObject.transform.position = position;
        viewObject.gameObject.SetActive(true);
    }
}
