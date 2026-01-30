using System;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveFacing", menuName = "Scriptable Objects/FacingDefinitions/MoveFacing")]
public class MoveFacingDefinition : FacingDefinition
{
    public override StrategyType<IFacingStrategy> ImplementationType 
        => new StrategyType<IFacingStrategy>(typeof(MoveFacingStrategy));
}

