using UnityEngine;

public interface IAnimationResolver
{
    AnimationType Resolve(
        IntentType intent,
        AttackPhase attackPhase,
        MoveData moveData
    );
}

