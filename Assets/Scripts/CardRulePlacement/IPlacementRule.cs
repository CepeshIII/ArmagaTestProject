public interface IPlacementRule
{
    bool Validate(Cell cell, CardData cardData);
}
