public class BattleEntityArchetype
{
    public int ArchetypeId { get; }
    public BattleEntityContext BaseContext { get; }
    public BattleEntityStrategySet BaseStrategies { get; }

    public BattleEntityArchetype(int archetypeId, BattleEntityContext baseContext, BattleEntityStrategySet baseStrategies)
    {
        ArchetypeId = archetypeId;
        BaseContext = baseContext;
        BaseStrategies = baseStrategies;
    }
}
