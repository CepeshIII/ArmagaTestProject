using Zenject;

public class AttackStrategyFactory : IFactory<AttackDefinition, IAttackStrategy> 
{
    private readonly DiContainer _container;


    [Inject]
    public AttackStrategyFactory(DiContainer container)
    {
        _container = container;
    }


    public IAttackStrategy Create(AttackDefinition definition)
    {
        return (IAttackStrategy)_container.Instantiate(definition.ImplementationType.Type);
    }

}
