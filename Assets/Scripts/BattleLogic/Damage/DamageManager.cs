using UnityEngine;
using Zenject;


public class DamageManager: MonoBehaviour, IDamageManager
{
    private IDamageDisplay damageDisplay;



    [Inject]
    public void Construct(IDamageDisplay damageDisplay)
    {
        this.damageDisplay = damageDisplay;
    }


    public void DealDamage(BattleEntity target, float amount)
    {
        target.TakeDamage(amount);
        damageDisplay.ShowDamage(amount, target.transform.position);
    }

}
