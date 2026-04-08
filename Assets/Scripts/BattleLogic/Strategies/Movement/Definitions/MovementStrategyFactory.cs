using Zenject;

public class MovementStrategyFactory : IFactory<MovementDefinition, IMovementStrategy>
{
    private readonly DiContainer _container;


    [Inject]
    public MovementStrategyFactory(DiContainer container)
    {
        _container = container;
    }


    public IMovementStrategy Create(MovementDefinition definition)
    {
        return (IMovementStrategy)_container.Instantiate(definition.ImplementationType.Type);
    }

}


