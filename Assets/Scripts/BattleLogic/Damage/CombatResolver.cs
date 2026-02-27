using UnityEngine;
using Zenject;


public class CombatResolver: MonoBehaviour, ICombatResolver
{
    private IDamageDisplay damageDisplay;



    [Inject]
    public void Construct(IDamageDisplay damageDisplay)
    {
        this.damageDisplay = damageDisplay;
    }


    public void Resolve(ref CombatPayload payload)
    {
        //ApplyCrit(ref payload);
        //ApplyOffenseModifiers(ref payload);
        //ApplyDefenseModifiers(ref payload);

        payload.Target.TakeDamage(payload.BaseDamage);
        //damageDisplay.ShowDamage(payload.BaseDamage, payload.Target.transform.position);
    }

}
