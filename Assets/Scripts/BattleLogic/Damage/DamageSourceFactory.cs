using UnityEngine;
using Zenject;

public class DamageSourceFactory
{
    private IDamageManager damageManager;


    [Inject]
    public DamageSourceFactory(IDamageManager damageManager)
    {
        this.damageManager = damageManager;
    }


    public void SpawnProjectile(GameObject owner, Vector3 position, Vector3 targetPosition, 
        AttackData attackData, ProjectileData projectileData) 
    {
        var projectileHolder = new GameObject(projectileData.name);
        var projectileRenderer = projectileHolder.AddComponent<SpriteRenderer>();
        var projectileCollider = projectileHolder.AddComponent<CircleCollider2D>();

        projectileRenderer.sprite = projectileData.sprite0;
        projectileCollider.offset = projectileData.colliderOffset;
        projectileCollider.radius = projectileData.colliderRadius;
        projectileCollider.includeLayers = LayerMask.GetMask("Units");

        projectileHolder.layer = 7;
    }
    
    
    public void SpawnStatic(GameObject owner, Vector3 position, Vector3 targetPosition, AttackData attackData) 
    {
        var rayCast2D = Physics2D.CircleCastAll(position, attackData.radius, Vector2.zero, 0f, LayerMask.GetMask("Units"));

        foreach (var hit in rayCast2D)
        {
            var victim = hit.rigidbody?.GetComponent<BattleEntity>();
            if(victim == null || victim.gameObject == owner)
                continue;

            Debug.Log(victim + " has been hit");
            damageManager.DealDamage(victim, attackData.attackDamage);
        }

    }

}
