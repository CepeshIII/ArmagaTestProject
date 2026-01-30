using UnityEngine;

public interface IAttackStrategy
{
    public void ExecuteAttack(BattleEntity entity, AttackContext attackContext);
    public void OnAttackHit(BattleEntity entity, AttackContext attackContext);
}
