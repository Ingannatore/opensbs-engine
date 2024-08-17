using System.Collections;
using OpenSBS.Engine.Models;
using OpenSBS.Engine.Models.Behaviours;

namespace OpenSBS.Engine.Modules;

public class SpaceshipModuleCollection : IEnumerable<ISpaceshipModule>, ITickable
{
    private readonly IDictionary<string, ISpaceshipModule> _modules = new Dictionary<string, ISpaceshipModule>();

    public ISpaceshipModule Get(string id) => _modules[id];
    public T? FirstOrDefault<T>() where T : ISpaceshipModule => _modules.Values.OfType<T>().FirstOrDefault();

    public IEnumerator<ISpaceshipModule> GetEnumerator() => _modules.Values.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void OnTick(World world, Entity owner, TimeSpan deltaT)
    {
        foreach (var module in _modules.Values)
        {
            module.OnTick(world, owner, deltaT);
        }
    }
}
