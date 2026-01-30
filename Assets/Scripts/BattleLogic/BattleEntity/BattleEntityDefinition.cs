using UnityEngine;


[CreateAssetMenu(fileName = "BattleEntityDefinition", menuName = "Scriptable Objects/BattleEntityDefinition")]
public class BattleEntityDefinition : ScriptableObject
{
    [Header("Core Data")]
    public BattleEntityData unitData;
    public AttackData attackData;
    public MovementData movementData;
    public HealthData healthData;

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
}

