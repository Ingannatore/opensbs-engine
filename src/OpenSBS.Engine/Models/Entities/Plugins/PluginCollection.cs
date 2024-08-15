using System.Collections;

namespace OpenSBS.Engine.Models.Entities.Plugins;

public class PluginCollection : IEnumerable<EntityPlugin>
{
    private readonly ISet<EntityPlugin> _plugins = new HashSet<EntityPlugin>();

    public IEnumerator<EntityPlugin> GetEnumerator() => _plugins.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void Add(EntityPlugin value) => _plugins.Add(value);
    public T? FirstOrDefault<T>() where T : EntityPlugin => _plugins.OfType<T>().FirstOrDefault();

    public void OnTick(TimeSpan deltaT, SpaceEntity owner, World world)
    {
        foreach (var plugin in _plugins)
        {
            plugin.OnTick(deltaT, owner, world);
        }
    }
}
