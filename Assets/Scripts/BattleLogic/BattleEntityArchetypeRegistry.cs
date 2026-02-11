using System.Collections.Generic;

public sealed class BattleEntityArchetypeRegistry
{
    private readonly Dictionary<int, BattleEntityArchetype> archetypes = new();


    public void Register(BattleEntityArchetype archetype)
    {
        if (!archetypes.ContainsKey(archetype.ArchetypeId))
        {
            archetypes[archetype.ArchetypeId] = archetype;
        }
    }


    public bool TryGet(int archetypeId, out BattleEntityArchetype archetype)
    {
        return archetypes.TryGetValue(archetypeId, out archetype);
    }


    public bool Contains(int archetypeId)
    {
        return archetypes.ContainsKey(archetypeId);
    }
}
