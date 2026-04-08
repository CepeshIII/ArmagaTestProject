using UnityEngine;
using Zenject;

public class SimpleMeleeBehavior : ICombatBehavior
{
    private readonly ITargetFinder targetFinder;


    [Inject]
    public SimpleMeleeBehavior(ITargetFinder targetFinder)
    {
        this.targetFinder = targetFinder;
    }


    public UnitIntent Decide(BattleEntity self, BattleEntityContext context)
    {
        var targetData = targetFinder.FindTarget(self.transform, self.Team);

        var hasTarget = targetData != null && targetData.Target != null;

        if (hasTarget)
        {
            var isTargetNearbyForAttack = targetData.Distance <= context.AttackData.attackDistance;

            if (isTargetNearbyForAttack)
            {
                return UnitIntent.Attack(targetData.Target);
            }
            else
            {
                return UnitIntent.MoveToTarget(targetData.Target);
            }
        }

        var isCloseToTargetPosition = Vector2.Distance(self.transform.position, self.EnemyBasePosition) <= context.MovementData.threshold;

        if (isCloseToTargetPosition) 
        {
            return UnitIntent.Idle();
        }

        return UnitIntent.MoveToPosition(self.EnemyBasePosition);

    }
}
