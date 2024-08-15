using System.Collections;

namespace OpenSBS.Engine.Models.Entities.Plugins;

public class PluginCollection : IEnumerable<EntityPlugin>
{
    private readonly ISet<EntityPlugin> _plugins = new HashSet<EntityPlugin>();

    public void Add(params EntityPlugin[] plugins) => _plugins.UnionWith(plugins);
    public T? FirstOrDefault<T>() where T : EntityPlugin => _plugins.OfType<T>().FirstOrDefault();

    public BodyPlugin GetBody() => FirstOrDefault<BodyPlugin>() ?? throw new Exception("Body plugin not found");
    public ModularPlugin GetModules() => FirstOrDefault<ModularPlugin>() ?? throw new Exception("Modular plugin not found");

    public void OnTick(TimeSpan deltaT, Celestial owner, World world)
    {
        foreach (var plugin in _plugins)
        {
            plugin.OnTick(deltaT, owner, world);
        }
    }

    public IEnumerator<EntityPlugin> GetEnumerator() => _plugins.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
