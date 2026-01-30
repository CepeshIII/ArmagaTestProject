using Zenject;

public class PathFinderFactory : IFactory<PathFinderDefinition, IPathFinder>
{
    private readonly DiContainer _container;


    public PathFinderFactory(DiContainer container)
    {
        _container = container;
    }


    public IPathFinder Create(PathFinderDefinition definition)
    {
        return (IPathFinder)_container.Instantiate(definition.ImplementationType.Type);
    }

}