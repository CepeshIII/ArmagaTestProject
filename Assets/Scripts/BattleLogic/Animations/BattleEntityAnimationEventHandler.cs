using System;
using UnityEngine;


public class BattleEntityAnimationEventHandler: MonoBehaviour
{

    public event Action OnAttackEvent;


    public void AnimationEvent_OnAttackHit()
    {
        Debug.Log("BattleEntityAnimationEventHandler: OnAttackHit called");
        OnAttackEvent?.Invoke();
    }

}
