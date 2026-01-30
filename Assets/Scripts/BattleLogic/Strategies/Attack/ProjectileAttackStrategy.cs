using UnityEngine;
using Zenject;

public class ProjectileAttackStrategy : BaseAttackStrategy
{
    private readonly DamageSourceFactory damageSourceFactory;
    private readonly ProjectileData projectileData;



    [Inject]
    public ProjectileAttackStrategy(DamageSourceFactory damageSourceFactory, ProjectileData projectileData)
    {
        this.damageSourceFactory = damageSourceFactory;
        this.projectileData = projectileData;
    }


    public override void OnAttackHit(BattleEntity entity, AttackContext attackContext)
    {
        attackContext.phase = AttackPhase.Recovery;
        var attackData = entity.Context.AttackData;
        var transform = entity.transform;

        if (attackContext.Target == null)
        {
            return;
        }

        var directionToTarget = (attackContext.Target.transform.position - transform.position).normalized;

        Debug.Log($"Attack hit event triggered: {transform.position}, Direction: {directionToTarget}");
        damageSourceFactory.SpawnProjectile(transform.gameObject, transform.position + directionToTarget * attackData.offset,
                                            attackContext.Target.transform.position, attackData, projectileData);
    }
}
