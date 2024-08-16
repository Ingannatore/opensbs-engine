using System.Collections;
using OpenSBS.Engine.Models.Behaviours;

namespace OpenSBS.Engine.Models.Plugins;

public class ModularPlugin : EntityPlugin, IEnumerable<IEntityModule>, ITickable
{
    private readonly IDictionary<string, IEntityModule> _modules = new Dictionary<string, IEntityModule>();

    public IEntityModule Get(string id) => _modules[id];
    public T? FirstOrDefault<T>() where T : IEntityModule => _modules.Values.OfType<T>().FirstOrDefault();

    public IEnumerator<IEntityModule> GetEnumerator() => _modules.Values.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void OnTick(World world, Entity owner, TimeSpan deltaT)
    {
        foreach (var module in _modules.Values)
        {
            module.OnTick(world, owner, deltaT);
        }
    }
}
