using Zenject;

public class TargetFinderFactory : IFactory<TargetFinderDefinition, ITargetFinder>
{
    private readonly DiContainer _container;


    public TargetFinderFactory(DiContainer container)
    {
        _container = container;
    }


    public ITargetFinder Create(TargetFinderDefinition definition)
    {
        return (ITargetFinder)_container.Instantiate(definition.ImplementationType.Type);
    }

}
