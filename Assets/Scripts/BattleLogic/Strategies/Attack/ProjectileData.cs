using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileData", menuName = "Scriptable Objects/ProjectileData")]
public class ProjectileData : ScriptableObject
{
    [Header("Sprites")]
    public Sprite sprite0;
    public Sprite sprite45;
    public Sprite sprite90;
    public Sprite sprite135;
    public Sprite sprite180;
    public Sprite sprite225;
    public Sprite sprite270;
    public Sprite sprite315;

    [Header("ColliderParam")]
    public Vector2 colliderOffset;
    public float colliderRadius;
}
