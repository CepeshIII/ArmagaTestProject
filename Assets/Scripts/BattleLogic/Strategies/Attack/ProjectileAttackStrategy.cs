using System;
using System.Linq;
using UnityEngine;
using Zenject;



public class ProjectileAttackStrategy : BaseAttackStrategy
{
    private readonly DamageSourceFactory damageSourceFactory;



    [Inject]
    public ProjectileAttackStrategy(DamageSourceFactory damageSourceFactory)
    {
        this.damageSourceFactory = damageSourceFactory;
    }


    public override void OnAttackHit(BattleEntity attacker, AttackData attackData, UnitIntent unitIntent,
        IAttackConfiguration attackConfiguration, AttackContext attackContext)
    {
        if (attackConfiguration is not IRangeAttackConfiguration rangeConfig)
        {
            var interfaces = attackConfiguration.GetType()
                .GetInterfaces()
                .Select(x => x.Name);

            throw new ArgumentException(
                $"Wrong attack config type. " +
                $"Expect: {nameof(IRangeAttackConfiguration)}, " +
                $"But get interfaces: {string.Join(", ", interfaces)}");
        }

        attackContext.phase = AttackPhase.Recovery;

        if (attackContext.Target == null)
        {
            return;
        }

        var attackerTransform = attacker.transform;
        var targetTransform = attackContext.Target.transform;
        var directionToTarget = (targetTransform.position - attackerTransform.position).normalized;

        var payload = new CombatPayload
        {
            Source = attacker,
            Target = unitIntent.Target,
            BaseDamage = attackData.attackDamage,
            DamageType = DamageType.Physical
        };

        var position = attackerTransform.position + 
            (Vector3)rangeConfig.ProjectileData.projectileOrigin + directionToTarget * attackData.offset;

        damageSourceFactory.SpawnProjectile(
            position, 
            targetTransform,
            rangeConfig.ProjectileData,
            payload
            );
    }
}
