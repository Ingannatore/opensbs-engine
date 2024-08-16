
using System.Collections;
using OpenSBS.Engine.Models.Modules;

namespace OpenSBS.Engine.Models.Plugins;

public class ModularPlugin : EntityPlugin, IEnumerable<IModule>
{
    private readonly IDictionary<string, IModule> _modules = new Dictionary<string, IModule>();

    public IModule Get(string id) => _modules[id];
    public T? FirstOrDefault<T>() where T : IModule => _modules.Values.OfType<T>().FirstOrDefault();

    public IEnumerator<IModule> GetEnumerator() => _modules.Values.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public override void OnTick(World world, Celestial owner, TimeSpan deltaT)
    {
        foreach (var module in _modules.Values)
        {
            module.Update(deltaT, owner, world);
        }
    }
}
