using UnityEngine;


public interface IDamageDisplay
{
    void ShowDamage(float damage, Vector2 position);
    void ShowHeal(float amount, Vector2 position);
}
