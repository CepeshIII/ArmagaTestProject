using UnityEngine;

public class NoAttackStrategy : IAttackStrategy
{

    public void ExecuteAttack(BattleEntity attacker, AttackData attackData, UnitIntent unitIntent,
        IAttackConfiguration attackConfiguration, AttackContext attackContext)
    {
        attackContext.phase = AttackPhase.None;
        attackContext.Target = null;
        attackContext.RechargeTimer = 0f;
    }


    public void OnAttackHit(BattleEntity attacker, AttackData attackData, UnitIntent unitIntent,
        IAttackConfiguration attackConfiguration, AttackContext attackContext)
    {
        attackContext.phase = AttackPhase.None;
        attackContext.Target = null;
        attackContext.RechargeTimer = 0f;
    }

}
