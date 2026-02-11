using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;


public class BattleEntityHolder
{
    private readonly Dictionary<int, List<int>> battleEntitiesByArchetypeIndex = new();
    private readonly Dictionary<int, BattleEntity> battleEntities = new();

    public IEnumerable<BattleEntity> AllUnits => battleEntities.Select(x => x.Value);



    public void AddEntity(BattleEntity entity)
    {
        var id = entity.GetInstanceID();
        var archetypeIndex = entity.ArchetypeId;

        if (!battleEntitiesByArchetypeIndex.ContainsKey(archetypeIndex))
        {
            battleEntitiesByArchetypeIndex[archetypeIndex] = new List<int>();
        }

        battleEntities[id] = entity;
        battleEntitiesByArchetypeIndex[archetypeIndex].Add(id);
    }


    public void RemoveEntity(BattleEntity entity)
    {
        var id = entity.GetInstanceID();
        var archetypeIndex = entity.ArchetypeId;

        battleEntities.Remove(id);

        if (battleEntitiesByArchetypeIndex.TryGetValue(archetypeIndex, out var list))
        {
            list.Remove(id);
        }
    }


    public IEnumerable<BattleEntity> GetEntitiesByTeam(Team team)
    {
        foreach(var entityContainer in battleEntities)
        {
            if(entityContainer.Value.Team == team)
            {
                yield return entityContainer.Value;
            }
        }

        //return battleEntities
        //    .Where(entity => entity.Value.Context.BattleEntityData.team == team)
        //    .Select(x => x.Value);
    }


    public IEnumerable<BattleEntity> GetEntitiesByArchetypeIndex(int archetypeIndex)
    {
        if(!battleEntitiesByArchetypeIndex.TryGetValue(archetypeIndex, out var entityIds))
        {
            yield break;
        }

        foreach(var id in entityIds)
        {
            yield return battleEntities[id];
        }
    }

}


public class DeactivatedEntityPool 
{
    private Dictionary<int, Stack<BattleEntity>> poolByArchetype = new();


    public void Push(BattleEntity entity)
    {
        if(!poolByArchetype.TryGetValue(entity.ArchetypeId, out var stack))
        {
            stack = new();
            poolByArchetype[entity.ArchetypeId] = stack;
        }

        stack.Push(entity);
    }


    public BattleEntity Pop(int archetypeId)
    {
        if (!poolByArchetype.TryGetValue(archetypeId, out var stack))
        {
            return null;
        }

        if(!stack.TryPop(out var entity))
        {
            return null;
        }

        return entity;
    }


    public bool TryPop(int archetypeId, out BattleEntity entity)
    {
        entity = null;

        if (poolByArchetype.TryGetValue(archetypeId, out var stack))
        {
            return stack.TryPop(out entity);
        }

        return false;
    }
}



public class UnitsManager: MonoBehaviour, IUnitManager
{
    private BattleEntityFactory battleEntityFactory;
    private BattleEntityArchetypeRegistry archetypeRegistry;

    private readonly BattleEntityHolder battleEntityHolder = new();
    private readonly DeactivatedEntityPool deactivatedEntityPool = new();

    public IEnumerable<BattleEntity> AllUnits => battleEntityHolder.AllUnits;

    public event Action<BattleEntity> OnEntityActivated;


    [Inject]
    public void Construct(BattleEntityFactory battleEntityFactory, BattleEntityArchetypeRegistry archetypeRegistry)
    {
        this.battleEntityFactory = battleEntityFactory;
        this.archetypeRegistry = archetypeRegistry;
    }


    public IEnumerable<BattleEntity> GetUnitsByTeam(Team team)
    {
        return battleEntityHolder.GetEntitiesByTeam(team);
    }


    public void Register(BattleEntity entity)
    {
        battleEntityHolder.AddEntity(entity);
        entity.OnDied += HandleEntityDeath;
    }


    public void DeactivateEntity(BattleEntity entity)
    {
        entity.gameObject.SetActive(false);
        deactivatedEntityPool.Push(entity);
        battleEntityHolder.RemoveEntity(entity);

        entity.OnDied -= HandleEntityDeath;
    }


    public void DeactivateEntities(List<BattleEntity> battleEntities)
    {
        foreach (var entity in battleEntities)
        {
            DeactivateEntity(entity);
        }
    }


    public List<BattleEntity> GetFreeEntities(BattleEntityDefinition entityDefinition, int count)
    {
        // Try to get cached entities from disabled and activate them
        var result = GetFromPool(entityDefinition.GetInstanceID(), count);
        ActivateAndRegisterEntities(result);

        // If not enough free entities, create new ones and register them
        var newEntities = CreateNewEntities(entityDefinition, count - result.Count);
        ActivateAndRegisterEntities(newEntities);

        result.AddRange(newEntities);
        return result;
    }


    public BattleEntity GetFreeEntity(BattleEntityDefinition entityDefinition)
    {
        // Try to get cached entities from disabled, if not found create a new one and register it
        if (!deactivatedEntityPool.TryPop(entityDefinition.GetInstanceID(), out var entity))
        {
            entity = CreateNewEntity(entityDefinition);
        }

        ActivateAndRegisterEntity(entity);
        return entity;
    }


    private List<BattleEntity> GetFromPool(int archetypeId, int count)
    {
        var result = new List<BattleEntity>();
        for (int i = 0; i < count; i++)
        {
            if (!deactivatedEntityPool.TryPop(archetypeId, out var entity))
                break;

            if(archetypeRegistry.TryGet(archetypeId, out var archetype))
            {
                entity.ResetDataToBase(archetype.BaseContext);
            }
            else
            {
                throw new InvalidOperationException($"Archetype with id {archetypeId} not found in registry.");
            }

            result.Add(entity);
        }
        return result;
    }


    private void ActivateAndRegisterEntities(IEnumerable<BattleEntity> entities)
    {
        foreach (var entity in entities)
        {
            ActivateAndRegisterEntity(entity);
        }
    }


    private void ActivateAndRegisterEntity(BattleEntity entity)
    {
        if(entity != null)
        {
            entity.gameObject.SetActive(true);
            Register(entity);
            OnEntityActivated?.Invoke(entity);
            return;
        }

        throw new ArgumentNullException(nameof(entity));
    }


    private List<BattleEntity> CreateNewEntities(BattleEntityDefinition entityDefinition, int count)
    {
        TryRegisterArchetype(entityDefinition);

        var result = new List<BattleEntity>();
        for (int i = 0; i < count; i++)
        {
            var newEntity = battleEntityFactory.Create(entityDefinition, Vector3.zero, Quaternion.identity);
            result.Add(newEntity);
        }

        return result;
    }


    private BattleEntity CreateNewEntity(BattleEntityDefinition entityDefinition)
    {
        TryRegisterArchetype(entityDefinition);
        return battleEntityFactory.Create(entityDefinition, Vector3.zero, Quaternion.identity);
    }


    private void TryRegisterArchetype(BattleEntityDefinition entityDefinition)
    {
        var archetypeId = entityDefinition.GetInstanceID();

        if (!archetypeRegistry.Contains(archetypeId))
        {
            var archetype = new BattleEntityArchetype(archetypeId, entityDefinition.GetEntityContext(), entityDefinition.GetStrategySet());
            archetypeRegistry.Register(archetype);
        }
    }


    private void HandleEntityDeath(object entity, EventArgs eventArgs)
    {
        DeactivateEntity((BattleEntity)entity);
    }
}
