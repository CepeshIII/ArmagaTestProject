public class CardViewHandlerFactory 
{ 
    public ICardViewHandler GetHandler(CardType cardType, CardPrefabFactory cardPrefabFactory, ILinearGrid grid)
    {
        switch (cardType) 
        {
            case CardType.Unit:
                return new UnitCardViewHandler(cardPrefabFactory, new RadiusPlacementStrategy(grid));
            case CardType.Building:
                return new BuildingCardViewHandler(cardPrefabFactory, new CenterPlacementStrategy(grid));
        }

        return null;    
    }
}
