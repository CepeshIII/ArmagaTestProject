using UnityEngine;

public interface IAttackStrategy
{
    public void ExecuteAttack(BattleEntity attacker, AttackData attackData, UnitIntent unitIntent,
        IAttackConfiguration attackConfiguration, AttackContext attackContext);
    public void OnAttackHit(BattleEntity attacker, AttackData attackData, UnitIntent unitIntent,
        IAttackConfiguration attackConfiguration, AttackContext attackContext);
}
