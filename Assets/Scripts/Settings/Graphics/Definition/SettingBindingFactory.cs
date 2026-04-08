using System;
using Zenject;

public class SettingBindingFactory
{
    private readonly DiContainer container;



    [Inject]
    public SettingBindingFactory(DiContainer container)
    {
        this.container = container;
    }


    public ISettingBinding Create(SettingDefinition definition)
    {
        return definition switch
        {
            ResolutionSettingDefinition => container.Instantiate<ResolutionBinding>(),
            FullScreenSettingDefinition => container.Instantiate<FullScreenBinding>(),
            FrameRateLimitDefinition => container.Instantiate<FrameRateLimitBinding>(),
            VSyncSettingDefinition => container.Instantiate<VSyncBinding>(),
            _ => throw new Exception($"No binding for {definition.GetType()}")
        };
    }
}