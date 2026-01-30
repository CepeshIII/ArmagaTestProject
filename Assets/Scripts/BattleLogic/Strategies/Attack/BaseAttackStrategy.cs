using UnityEngine;

public abstract class BaseAttackStrategy : IAttackStrategy
{
    public void ExecuteAttack(BattleEntity entity, AttackContext attackContext)
    {
        var rechargeTimer = attackContext.RechargeTimer;
        var attackPhase = attackContext.phase;
        var attackData = entity.Context.AttackData;
        var target = entity.CurrentIntent.Target;
        var transform = entity.transform;

        if (rechargeTimer < 0)
        {
            if (target != null && target != null)
            {
                var distanceToTarget = Vector2.Distance(transform.position, target.transform.position);
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

    public abstract void OnAttackHit(BattleEntity entity, AttackContext attackContext);
}
