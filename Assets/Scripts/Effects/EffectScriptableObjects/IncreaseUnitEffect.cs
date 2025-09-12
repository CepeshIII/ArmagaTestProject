using UnityEngine;


[CreateAssetMenu(menuName = "Scriptable Objects/Effects/IncreaseUnitEffect")]
public class IncreaseUnitEffect : EffectData
{
    public int increaseAmount;

    public override void ApplyEffect(CardInstance cardInstance)
    {
        if(cardInstance.Data.CardType == CardType.Unit)
        {
            var unitCardInstance = cardInstance as UnitCardInstance;
            unitCardInstance.CurrentUnitCount += increaseAmount;
        }
    }
}
