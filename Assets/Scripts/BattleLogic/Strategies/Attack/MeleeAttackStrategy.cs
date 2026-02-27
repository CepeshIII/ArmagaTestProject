using UnityEngine;
using Zenject;




public class InstantMeleeAttack : IAttackStrategy
{
    private readonly DamageSourceFactory damageSourceFactory;



    [Inject]
    public InstantMeleeAttack(DamageSourceFactory damageSourceFactory)
    {
        this.damageSourceFactory = damageSourceFactory;
    }


    public void ExecuteAttack(BattleEntity attacker, AttackData attackData, UnitIntent unitIntent,
        IAttackConfiguration attackConfiguration, AttackContext attackContext)
    {
        OnAttackHit(attacker, attackData, unitIntent, attackConfiguration, attackContext);
    }



    public void OnAttackHit(BattleEntity attacker, AttackData attackData, UnitIntent unitIntent,
        IAttackConfiguration attackConfiguration, AttackContext attackContext)
    {
        attackContext.phase = AttackPhase.Recovery;

        if (attackContext.Target == null)
        {
            return;
        }

        var attackerTransform = attacker.transform;
        var targetTransform = attackContext.Target.transform;
        var directionToTarget = (targetTransform.position - attackerTransform.position).normalized;
        var position = attackerTransform.position + directionToTarget * attackData.offset;
        var payload = new CombatPayload
        {
            Source = attacker,
            Target = unitIntent.Target,
            BaseDamage = attackData.attackDamage,
            DamageType = DamageType.Physical
        };

        Debug.Log($"Attack hit event triggered: {attackerTransform.position}, Direction: {directionToTarget}");
        damageSourceFactory.SpawnStatic(position, targetTransform, payload, attackData);
    }

}



public class MeleeAttackStrategy : BaseAttackStrategy
{
    private readonly ICombatResolver damageManager;
    private readonly DamageSourceFactory damageSourceFactory;



    [Inject]
    public MeleeAttackStrategy(ICombatResolver damageManager, DamageSourceFactory damageSourceFactory)
    {
        this.damageSourceFactory = damageSourceFactory;
        this.damageManager = damageManager;
    }


    public override void OnAttackHit(BattleEntity attacker, AttackData attackData, UnitIntent unitIntent,
        IAttackConfiguration attackConfiguration, AttackContext attackContext)
    {
        attackContext.phase = AttackPhase.Recovery;

        if (attackContext.Target == null )
        {
            return;
        }
        var attackerTransform = attacker.transform;
        var targetTransform = attackContext.Target.transform;
        var directionToTarget = (targetTransform.position - attackerTransform.position).normalized;
        var position = attackerTransform.position + directionToTarget * attackData.offset;
        var payload = new CombatPayload
        {
            Source = attacker,
            Target = unitIntent.Target,
            BaseDamage = attackData.attackDamage,
            DamageType = DamageType.Physical
        };


        Debug.Log($"Attack hit event triggered: {attackerTransform.position}, Direction: {directionToTarget}");
        damageSourceFactory.SpawnStatic(position, targetTransform, payload, attackData);
    }
}
