using System;
using UnityEngine;


[Serializable]
public struct AttackData
{
    public float attackDistance;
    public float attackDamage;
    public float rechargeTime;

    public float offset;
    public float radius;
}


public interface IAttackConfiguration
{

}


public interface IRangeAttackConfiguration : IAttackConfiguration
{
    ProjectileData ProjectileData { get; }
}


public interface IMeleeAttackConfiguration: IAttackConfiguration
{


}


[Serializable]
public class RangeAttackConfiguration: IRangeAttackConfiguration
{
    public string m_Description = "Ripe";

    [SerializeField]
    public ProjectileData projectileData;

    public ProjectileData ProjectileData => projectileData;
}


[Serializable]
public class MeleeAttackConfiguration: IMeleeAttackConfiguration
{
    public bool m_IsRound = true;
}