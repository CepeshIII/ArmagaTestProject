using UnityEngine;


public readonly struct UnitIntent
{
    public readonly IntentType Type { get; }
    public readonly IMovementIntent Movement { get; }
    public readonly BattleEntity Target { get; }

    public readonly bool HasMovement => Movement != null;
    public readonly bool HasTarget => Target != null;



    private UnitIntent(IntentType type, IMovementIntent movement, BattleEntity target)
    {
        Type = type;
        Movement = movement;
        Target = target;
    }


    public static UnitIntent Idle() 
        => new (IntentType.Idle, null, null);

    public static UnitIntent MoveToTarget(BattleEntity target) 
        => new (IntentType.MoveToTarget, new MoveToTargetIntent(target), target);

    public static UnitIntent MoveToPosition(Vector2 position) 
        => new (IntentType.MoveToPosition, new MoveToPositionIntent(position), null);

    public static UnitIntent Attack(BattleEntity target) 
        => new (IntentType.Attack, null, target);
}
