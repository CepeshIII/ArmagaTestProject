using UnityEngine;
using Zenject;


public class MeleeAttackStrategy : BaseAttackStrategy
{
    private readonly IDamageManager damageManager;
    private readonly DamageSourceFactory damageSourceFactory;



    [Inject]
    public MeleeAttackStrategy(IDamageManager damageManager, DamageSourceFactory damageSourceFactory)
    {
        this.damageSourceFactory = damageSourceFactory;
        this.damageManager = damageManager;
    }


    public override void OnAttackHit(BattleEntity entity, AttackContext attackContext)
    {
        attackContext.phase = AttackPhase.Recovery;
        var attackData = entity.Context.AttackData;
        var transform = entity.transform;


        if (attackContext.Target == null )
        {
            return;
        }

        var directionToTarget = (attackContext.Target.transform.position - transform.position).normalized;
        Debug.Log($"Attack hit event triggered: {transform.position}, Direction: {directionToTarget}");
        damageSourceFactory.SpawnStatic(transform.gameObject, transform.position + directionToTarget * attackData.offset,
                                            attackContext.Target.transform.position, attackData);
    }
}
