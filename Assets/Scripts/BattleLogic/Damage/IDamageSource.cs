using System.Collections.Generic;
using UnityEngine;


public interface IDamageSource
{
}


public struct DamagePayload
{
    public GameObject Owner;
    public float Amount;
    public Team Team;
}


public enum DamageType
{
    Physical,
    Magical,
    Pure
}

public interface ICombatModifier
{

}

public struct CombatPayload
{
    public BattleEntity Source;
    public BattleEntity Target;

    public float BaseDamage;

    public DamageType DamageType;
    public bool CanCrit;
    public float CritChance;
    public float CritMultiplier;

    public List<ICombatModifier> Modifiers;
}