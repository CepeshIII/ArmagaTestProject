using UnityEngine;
using Zenject;

public class RegistryLineupPositionProvider : ILineupPositionProvider
{
    private readonly LineupEntityRegistry registry;


    [Inject]
    public RegistryLineupPositionProvider(LineupEntityRegistry registry)
    {
        this.registry = registry;
    }


    public Vector3 GetPosition(BattleEntity entity)
    {
        return registry.TryGet(entity, out var pos)
            ? pos
            : entity.transform.position;
    }
}
