using System.Collections;

namespace OpenSBS.Engine.Models.Modules;

public class ModuleCollection : IEnumerable<IModule>
{
    private readonly ICollection<IModule> _modules = new List<IModule>();
    private readonly IDictionary<string, IModule> _modulesIndex = new Dictionary<string, IModule>();

    public IModule Get(string id) => _modulesIndex[id];
    public T? FirstOrDefault<T>() where T : IModule => _modules.OfType<T>().FirstOrDefault();
    public IEnumerator<IModule> GetEnumerator() => _modules.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void Add(IModule module)
    {
        _modules.Add(module);
        _modulesIndex.Add(module.Id, module);
    }

    public void Remove(IModule module)
    {
        _modules.Remove(module);
        _modulesIndex.Remove(module.Id);
    }

    public void Update(TimeSpan deltaT, SpaceEntity owner, World world)
    {
        foreach (var module in _modules)
        {
            module.Update(deltaT, owner, world);
        }
    }
}
