using System.Collections;
using OpenSBS.Engine.Behaviours;
using OpenSBS.Engine.Entities;

namespace OpenSBS.Engine.Modules;

public class SpaceshipModuleCollection : IEnumerable<SpaceshipModule>
{
    private readonly IDictionary<Guid, SpaceshipModule> _modules;

    public SpaceshipModuleCollection(SpaceshipModule[]? modules = null)
    {
        _modules = modules?.ToDictionary(module => module.Id, module => module) ?? [];
    }

    public SpaceshipModule Get(Guid id) => _modules[id];
    public T? FirstOrDefault<T>() where T : SpaceshipModule => _modules.Values.OfType<T>().FirstOrDefault();

    public IEnumerator<SpaceshipModule> GetEnumerator() => _modules.Values.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void OnTick(World world, Spaceship owner, TimeSpan deltaT)
    {
        foreach (var module in _modules.Values)
        {
            module.OnTick(world, owner, deltaT);
        }
    }
}
