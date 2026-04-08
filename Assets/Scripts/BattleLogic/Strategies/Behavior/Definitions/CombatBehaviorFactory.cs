using Zenject;

public class CombatBehaviorFactory : IFactory<CombatBehaviorDefinition, ICombatBehavior>
{
    private readonly DiContainer _container;


    [Inject]
    public CombatBehaviorFactory(DiContainer container)
    {
        _container = container;
    }


    public ICombatBehavior Create(CombatBehaviorDefinition definition)
    {
        return (ICombatBehavior)_container.Instantiate(definition.ImplementationType.Type);
    }

}


