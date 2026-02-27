using UnityEngine;


public class ProjectileBullet: MonoBehaviour 
{
    [SerializeField]
    private MovementData movementData;
    
    [SerializeField]
    private AttackData attackData;

    // Strategies (runtime-swappable)
    private IPathFinder pathFinder = new CartesianStraightPathFinder();
    private IMovementStrategy movementStrategy = new CartesianMovementStrategy();
    private IAttackStrategy attackStrategy = new NoAttackStrategy();

    private UnitIntent currentUnitIntent;

    private Animator animator;
    private float timer = 10f;


    private void Update()
    {
        if(timer < 0)
        {
        //    Destroy(gameObject);
        }
        timer -= Time.time;

        TickMovement();
        //attackStrategy.ExecuteAttack(transform, attackData, currentUnitIntent, null, null);
    }


    private void TickMovement()
    {
        if (!currentUnitIntent.HasMovement)
            return;

        if (!currentUnitIntent.Movement.TryGetDestination(out var destination))
            return;

        var path = pathFinder.FindPath(transform, destination);

        var currentMoveData = movementStrategy.Move(
            transform,
            movementData,
            path
        );
    }


    public void SetAnimator(Animator animator, AnimatorOverrideController animatorController)
    {
        this.animator = animator;
        animator.runtimeAnimatorController = animatorController;
    }


    public void SetDirection(Vector2 direction)
    {
        direction = direction.normalized;
        animator.SetFloat("DirectionX", direction.x);
        animator.SetFloat("DirectionY", direction.y);

        currentUnitIntent = UnitIntent.MoveToPosition(direction * float.MaxValue);
    }


    public void SetMovementData(MovementData data)
    {
        movementData = data;
    }


    private void OnDrawGizmos()
    {
        currentUnitIntent.Movement.TryGetDestination(out var position);
        var direction = (position - transform.position).normalized;
        Gizmos.DrawLine(transform.position, transform.position + direction);
    }
}
