using UnityEngine;

public abstract class BaseAttackStrategy : IAttackStrategy
{
    public void ExecuteAttack(BattleEntity attacker, AttackData attackData, UnitIntent unitIntent,
        IAttackConfiguration attackConfiguration, AttackContext attackContext)
    {
        var rechargeTimer = attackContext.RechargeTimer;
        var attackerTransform = attacker.transform;
        var attackPhase = attackContext.phase;
        var target = unitIntent.Target;

        if (rechargeTimer < 0)
        {
            if (target != null && target != null)
            {
                var distanceToTarget = Vector2.Distance(attackerTransform.position, target.transform.position);
                if (distanceToTarget <= attackData.attackDistance)
                {
                    rechargeTimer = attackData.rechargeTime;
                    attackPhase = AttackPhase.Windup;
                }
            }
            else
            {
                attackPhase = AttackPhase.None;
            }
        }
        else
        {
            attackPhase = attackContext.phase;
            rechargeTimer -= Time.deltaTime;
        }

        attackContext.RechargeTimer = rechargeTimer;
        attackContext.phase = attackPhase;
        attackContext.Target = target;
    }

    public abstract void OnAttackHit(BattleEntity attacker, AttackData attackData, UnitIntent unitIntent,
        IAttackConfiguration attackConfiguration, AttackContext attackContext);
}
