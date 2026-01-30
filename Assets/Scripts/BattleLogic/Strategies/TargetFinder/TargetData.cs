using UnityEngine;

public class TargetData
{
    private readonly BattleEntity targetEntity;
    private readonly Transform transform;
    private readonly float distance;
    private readonly Vector3 direction;

    public BattleEntity Target => targetEntity;
    public Transform Transform => transform;
    public float Distance => distance;
    public Vector3 Direction => direction;


    public TargetData(BattleEntity targetEntity, Transform transform, float distance, Vector3 direction)
    {
        this.targetEntity = targetEntity;
        this.transform = transform;
        this.distance = distance;
        this.direction = direction.normalized;
    }
}
