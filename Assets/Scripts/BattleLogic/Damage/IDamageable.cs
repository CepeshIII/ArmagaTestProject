using System;
using UnityEngine;

public interface IDamageable
{
    public event EventHandler<float> OnDamaged;
    public event EventHandler OnDied;

    public void TakeDamage(float damageAmount);
}
