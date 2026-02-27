using UnityEngine;

[CreateAssetMenu(fileName = "MoveAndAttackFacing", menuName = "Scriptable Objects/FacingDefinitions/MoveAndAttackFacing")]
public class MoveAndAttackFacingDefinition : FacingDefinition
{
    public override StrategyType<IFacingStrategy> ImplementationType
        => StrategyType<IFacingStrategy>.From<MoveAndAttackFacingStrategy>();
}
