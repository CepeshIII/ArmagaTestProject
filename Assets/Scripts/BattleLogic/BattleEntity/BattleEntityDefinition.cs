using System;
using UnityEngine;


[Serializable]
public sealed class EntityCapabilities
{
    public bool CanMove = true;
    public bool CanAttack = true;
    public bool CanDecide = true;



    public void Reset()
    {
        CanMove = true;
        CanAttack = true;
        CanDecide = true;
    }


    public EntityCapabilities Clone()
    {
        return new EntityCapabilities
        {
            CanMove = CanMove,
            CanAttack = CanAttack,
            CanDecide = CanDecide
        };
    }
}


[CreateAssetMenu(fileName = "BattleEntityDefinition", menuName = "Scriptable Objects/BattleEntityDefinition")]
public class BattleEntityDefinition : ScriptableObject
{
    [Header("Core Data")]
    public AttackData attackData;
    public MovementData movementData;
    public HealthData healthData;
    public EntityCapabilities capabilities;

    [SerializeReference, SubclassSelector]
    public IAttackConfiguration attackConfiguration;

    [Header("Visuals")]
    public AnimatorOverrideController animator;
    public BattleEntity prefab;

    [Header("Behavior Definitions")]
    public AttackDefinition attack;
    public CombatBehaviorDefinition behavior;
    public FacingDefinition facing;
    public MovementDefinition movement;
    public PathFinderDefinition pathFinder;
    public TargetFinderDefinition targetFinder;
    public AnimationResolverDefinition animationResolver;


    public BattleEntityContext GetEntityContext() => new (attackData, movementData, healthData);
    

    public BattleEntityStrategySet GetStrategySet() =>
        new BattleEntityStrategySet()
            .SetCombatBehavior(behavior.ImplementationType)
            .SetAttackStrategy(attack.ImplementationType)
            .SetPathFinder(pathFinder.ImplementationType)
            .SetTargetFinder(targetFinder.ImplementationType)
            .SetMovementStrategy(movement.ImplementationType)
            .SetFacingStrategy(facing.ImplementationType)
            .SetAnimationResolver(animationResolver.ImplementationType);
}

