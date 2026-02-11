public class CardViewHandlerFactory 
{ 
    private UnitsManager unitsManager;
    private BoardEntityRegistry boardEntityRegistry;


    public CardViewHandlerFactory(UnitsManager unitsManager, BoardEntityRegistry boardEntityRegistry)
    {
        this.unitsManager = unitsManager;
        this.boardEntityRegistry = boardEntityRegistry;
    }


    public ICardViewHandler GetHandler(CardType cardType, CardPrefabFactory cardPrefabFactory, ILinearGrid grid)
    {
        switch (cardType) 
        {
            case CardType.Unit:
                return new BattleEntityCardViewHandler(unitsManager, new RadiusPlacementStrategy(grid), boardEntityRegistry);
            //return new UnitCardViewHandler(cardPrefabFactory, new RadiusPlacementStrategy(grid));
            case CardType.Building:
                return new BuildingCardViewHandler(cardPrefabFactory, new CenterPlacementStrategy(grid));
        }

        return null;    
    }
}
