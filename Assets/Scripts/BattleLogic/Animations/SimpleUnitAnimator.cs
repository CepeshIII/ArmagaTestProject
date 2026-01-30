using UnityEngine;

public class SimpleUnitAnimator : IUnitAnimator
{


    public void PlayMoveAnimation(Animator animator, AnimationType unitEvent, Vector3 direction)
    {
        var dir = new Vector2(direction.x, direction.y).normalized;
        animator.SetFloat("DirectionX", dir.x);
        animator.SetFloat("DirectionY", dir.y);

        animator.ResetTrigger("Idle");
        animator.ResetTrigger("Run");
        animator.ResetTrigger("Attack");

        switch (unitEvent)
        {
            case AnimationType.Idle:
                animator.SetTrigger("Idle");
                break;
            case AnimationType.Run:
                animator.SetTrigger("Run");
                break;
            case AnimationType.Attack:
                animator.SetTrigger("Attack");
                break;
            default:
                animator.Play("Idle");
                break;
        }

    }
}
