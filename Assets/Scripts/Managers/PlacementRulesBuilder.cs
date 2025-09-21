public static class PlacementRulesBuilder
{

    public static PlacementValidator CreateDefault()
    {
        var placementValidator = new PlacementValidator();

        // Add rules to PlacementValidator
        placementValidator.AddMandatoryRule(new PlacementRules.CellIsNotNullRule());
        placementValidator.AddMandatoryRule(new PlacementRules.CellAvailableRule());

        placementValidator.AddOptionalRule(new PlacementRules.CellEmptyRule());
        placementValidator.AddOptionalRule(new PlacementRules.SameCardRule());

        return placementValidator;
    }
}