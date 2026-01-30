public class DefaultAnimationResolver : IAnimationResolver
{
    public AnimationType Resolve(IntentType intent, AttackPhase attackPhase, MoveData moveData)
    {
        if(intent == IntentType.Attack)
        {
            // Attack takes priority
            if (attackPhase == AttackPhase.Windup)
                return AnimationType.Attack;
        }
        else if(intent == IntentType.MoveToPosition || intent == IntentType.MoveToTarget)
        {
            // Moving animation next
            if (moveData.isMoving)
                return AnimationType.Run;
        }

        // Otherwise idle
        return AnimationType.Idle;
    }
}
