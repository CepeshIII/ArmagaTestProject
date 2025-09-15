public interface IPlacementRule
{
    bool Validate(GameBoard.Cell cell, CardData cardData);
}
