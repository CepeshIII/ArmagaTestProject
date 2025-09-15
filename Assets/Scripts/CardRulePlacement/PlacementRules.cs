namespace PlacementRules
{
    // Rule 1 – inside grid bounds(actually best handled before fetching cell)
    public class CellIsNotNullRule : IPlacementRule
    {
        public bool Validate(GameBoard.Cell cell, CardData cardData)
        {
            return cell != null; // if we couldn’t fetch a valid cell, it's out of bounds
        }
    }


    // Rule 2 – cell is available
    public class CellAvailableRule : IPlacementRule
    {
        public bool Validate(GameBoard.Cell cell, CardData cardData)
        {
            return cell.isAvailable;
        }
    }


    // Rule 3 – cell empty
    public class CellEmptyRule : IPlacementRule
    {
        public bool Validate(GameBoard.Cell cell, CardData cardData)
        {
            return cell.cards.Count == 0;
        }
    }


    // Rule 4 – same card already on cell
    public class SameCardRule : IPlacementRule
    {
        public bool Validate(GameBoard.Cell cell, CardData cardData)
        {
            return cell.cards.Exists(c => c.Data.cardId == cardData.cardId);
        }
    }

    /* This rule checks if the new card can be placed with existing cards based on custom compatibility logic defined in CardData.
       You would need to implement the CanBePlacedWith method in your CardData class to define specific compatibility rules.
    
    // Rule 5 – custom card data compatibility
    public class CardCompatibilityRule : IPlacementRule
    {
        public bool Validate(GameBoard.Cell cell, CardData cardData)
        {
            foreach (var existingCard in cell.cards)
            {
                if (!cardData.CanBePlacedWith(existingCard.Data))
                    return false;
            }
            return true;
        }
    }

    */

}

