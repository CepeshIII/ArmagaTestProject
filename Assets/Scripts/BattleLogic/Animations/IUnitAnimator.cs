using UnityEngine;

public interface IUnitAnimator
{
    void PlayMoveAnimation(Animator animator, AnimationType unitEvent, Vector3 direction);
}