using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileData", menuName = "Scriptable Objects/ProjectileData")]
public class ProjectileData : ScriptableObject
{
    public Vector2 projectileOrigin;
    public MovementData movementData;
    public AttackData attackData;

    public AnimatorOverrideController animator;

    public float speed;
    public GameObject prefab;

    [Header("ColliderParam")]
    public Vector2 colliderOffset;
    public float colliderRadius;
}
