using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class BattleEntityCardViewHandler : ICardViewHandler
{
    private List<BattleEntity> viewObjects = new();

    private readonly UnitsManager battleEntityManager;
    private readonly ICellPlacementStrategy placementStrategy;
    private readonly BoardEntityRegistry boardEntityRegistry;
    
    public CardInstance Instance { get; private set; }
    public BattleEntityDefinition definition { get; private set; }
    public BoardCellPosition Position { get; private set; }



    public BattleEntityCardViewHandler(UnitsManager unitManager, 
        ICellPlacementStrategy placementStrategy, BoardEntityRegistry boardEntityRegistry)
    {
        this.battleEntityManager = unitManager;
        this.placementStrategy = placementStrategy;
        this.boardEntityRegistry = boardEntityRegistry;
    }


    public void CreateView(CardInstance instance, Transform parent)
    {
        if (instance is UnitCardInstance unitInstance)
        {
            Instance = instance;
            definition = unitInstance.entityDefinition;
            RemoveView();
        }
    }


    public void RemoveView()
    {
        battleEntityManager.DeactivateEntities(viewObjects);

        foreach (var viewObject in viewObjects)
        {
            boardEntityRegistry.Unregister(viewObject);
        }

        viewObjects.Clear();
    }


    public void UpdateView()
    {
        if (Instance is not UnitCardInstance unitInstance)
            return;

        RemoveView();

        var positions = placementStrategy.GetPositions(
            Instance.IndexCoords, 
            unitInstance.CurrentUnitCount
            );

        viewObjects = battleEntityManager.GetFreeEntities(
            definition, 
            positions.Count()
            );

        foreach (var (entity, index) in viewObjects.Select((value, i) => (value, i)))
        {
            Debug.Log($"value: {entity}, index: {index}, position: {positions[index]}");
            
            var position = positions[index];
            boardEntityRegistry.Register(entity, Position, position);

            entity.SetTeam(Team.Player);
            entity.transform.position = positions[index];
            entity.gameObject.SetActive(true);
        }
    }
}
