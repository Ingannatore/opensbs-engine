using System.Collections;

namespace OpenSBS.Engine.Models.Entities.Plugins;

public class PluginCollection : IEnumerable<IEntityPlugin>
{
    private readonly IDictionary<string, IEntityPlugin> _plugins = new Dictionary<string, IEntityPlugin>();

    public IEnumerator<IEntityPlugin> GetEnumerator() => _plugins.Values.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void Add(string key, IEntityPlugin value) => _plugins.Add(key, value);
    public void Remove(string key) => _plugins.Remove(key);
    public bool ContainsKey(string key) => _plugins.ContainsKey(key);
    public T? FirstOrDefault<T>(string key) where T : IEntityPlugin => _plugins.ContainsKey(key) ? (T)_plugins[key] : default;

    public void OnTick(TimeSpan deltaT, SpaceEntity owner, World world)
    {
        foreach (var plugin in _plugins.Values)
        {
            plugin.OnTick(deltaT, owner, world);
        }
    }
}
