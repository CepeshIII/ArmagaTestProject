using UnityEngine;
using Zenject;



public class DamageSourceFactory
{
    private ICombatResolver combatResolver;
    private readonly DiContainer container;



    [Inject]
    public DamageSourceFactory(ICombatResolver combatResolver, DiContainer container)
    {
        this.combatResolver = combatResolver;
        this.container = container;
    }
    
    
    public void SpawnStatic(Vector3 origin, Transform target, 
        CombatPayload payload, AttackData attackData) 
    {
        var meleeDamageSource = new MeleeDamageSource();
        meleeDamageSource.Initialize(origin,
            target,
            attackData,
            payload,
            combatResolver);
    }


    public void SpawnProjectile(
        Vector3 origin,
        Transform target,
        ProjectileData projectileData,
        CombatPayload payload)
    {
        var projectileGO = container.InstantiatePrefab(
            projectileData.prefab);

        var projectile =
            projectileGO.GetComponent<ProjectileDamageSource>();

        projectile.Initialize(
            origin,
            target,
            projectileData,
            payload,
            combatResolver);
    }

}

