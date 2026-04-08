using Zenject;

public class FacingFactory : IFactory<FacingDefinition, IFacingStrategy>
{
    private readonly DiContainer _container;


    [Inject]
    public FacingFactory(DiContainer container)
    {
        _container = container;
    }


    public IFacingStrategy Create(FacingDefinition definition)
    {
        return (IFacingStrategy)_container.Instantiate(definition.ImplementationType.Type);
    }

}

