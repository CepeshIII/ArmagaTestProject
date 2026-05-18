using System.Collections.Generic;
using UnityEngine;



public abstract class CardInstance
{
    public CardData Data { get; }
    public Vector2Int IndexCoords { get; private set; }
    public Team Team { get; set; }

    private List<BattleEntity> _linkedEntities = new();


    public CardInstance(CardData data, Vector2Int indexCoords, Team team)
    {
        Data = data;
        IndexCoords = indexCoords;
        Team = team;
    }


    public void Move(Vector2Int newCoords)
    {
        IndexCoords = newCoords;
    }


    public void SetTeam(Team team)
    {
        Team = team;
    }


    public void AddLinkedEntity(BattleEntity entity)
    {
        if (!_linkedEntities.Contains(entity))
            _linkedEntities.Add(entity);
    }


    public void RemoveLinkedEntity(BattleEntity entity)
    {
        _linkedEntities.Remove(entity);
    }


    public IReadOnlyList<BattleEntity> GetLinkedEntities() => _linkedEntities;


    public void PropagateStatChange(EffectStatTarget stat, float value, EffectStackType stackType)
    {
        foreach (var entity in _linkedEntities)
        {
            ApplyStatToEntity(entity, stat, value, stackType);
        }
    }


    private void ApplyStatToEntity(BattleEntity entity, EffectStatTarget stat, float value, EffectStackType stackType)
    {
        switch (stat)
        {
            case EffectStatTarget.AttackDamage:
                var attack = entity.Context.AttackData;
                attack.attackDamage = stackType switch
                {
                    EffectStackType.Additive => attack.attackDamage + value,
                    EffectStackType.Multiplicative => attack.attackDamage * value,
                    EffectStackType.Override => value,
                    _ => attack.attackDamage
                };
                entity.Context.SetAttackData(attack);
                break;

            case EffectStatTarget.Speed:
                var movement = entity.Context.MovementData;
                movement.speed = stackType switch
                {
                    EffectStackType.Additive => movement.speed + value,
                    EffectStackType.Multiplicative => movement.speed * value,
                    EffectStackType.Override => value,
                    _ => movement.speed
                };
                entity.Context.SetMovementData(movement);
                break;

            case EffectStatTarget.Health:
                var health = entity.Context.HealthData;
                health.health = stackType switch
                {
                    EffectStackType.Additive => health.health + value,
                    EffectStackType.Multiplicative => health.health * value,
                    EffectStackType.Override => value,
                    _ => health.health
                };
                entity.Context.SetHealthData(health);
                break;
        }
    }


    public abstract void ResetParam();
    public abstract IEnumerable<string> GetDescription();
}
