using System.Collections.Generic;
using UnityEngine;


public class PlacementValidator
{
    private readonly GameBoard gameBoard;
    private readonly List<IPlacementRule> mandatoryRules = new();
    private readonly List<IPlacementRule> optionalRules = new();

    public void AddMandatoryRule(IPlacementRule rule) => mandatoryRules.Add(rule);
    public void AddOptionalRule(IPlacementRule rule) => optionalRules.Add(rule);



    public PlacementValidator(GameBoard gameBoard) 
    {
        this.gameBoard = gameBoard;
    }


    public bool CanPlace(Vector2Int indexCoords, CardData card)
    {
        if(!gameBoard.TryGetCell(indexCoords, out var cell))
        {
            return false;
        }

        // All mandatory rules must pass
        foreach (var rule in mandatoryRules)
        {
            if (!rule.Validate(cell, card))
                return false;
        }

        // At least one optional rule must pass
        if (optionalRules.Count > 0)
        {
            bool passed = false;
            foreach (var rule in optionalRules)
            {
                if (rule.Validate(cell, card))
                {
                    passed = true;
                    break;
                }
            }
            if (!passed) return false;
        }

        return true;
    }
}

