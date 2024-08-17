using System.Collections;
using OpenSBS.Engine.Models.Behaviours;
using OpenSBS.Engine.Modules;

namespace OpenSBS.Engine.Models.Plugins;

public class PluginCollection : IEnumerable<EntityPlugin>, ITickable
{
    private readonly ISet<EntityPlugin> _plugins = new HashSet<EntityPlugin>();

    public void Add(params EntityPlugin[] plugins) => _plugins.UnionWith(plugins);
    public T? FirstOrDefault<T>() where T : EntityPlugin => _plugins.OfType<T>().FirstOrDefault();

    public BodyPlugin GetBody() => FirstOrDefault<BodyPlugin>() ?? throw new Exception("Body plugin not found");
    public StructurePlugin GetStructure() => FirstOrDefault<StructurePlugin>() ?? throw new Exception("Structure plugin not found");
    public SpaceshipModuleCollection GetModules() => FirstOrDefault<SpaceshipModuleCollection>() ?? throw new Exception("Modular plugin not found");

    public void OnTick(World world, Entity owner, TimeSpan deltaT)
    {
        foreach (var plugin in _plugins.OfType<ITickable>())
        {
            plugin.OnTick(world, owner, deltaT);
        }
    }

    public IEnumerator<EntityPlugin> GetEnumerator() => _plugins.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
