using OpenSBS.Engine.Models.Actions;
using OpenSBS.Engine.Models.Entities.Plugins;
using OpenSBS.Engine.Models.Modules;
using OpenSBS.Engine.Models.Templates;

namespace OpenSBS.Engine.Models;

public abstract class Celestial : Entity
{
    public ModuleCollection Modules { get; } = [];
    public PluginCollection Plugins { get; } = [];

    public Celestial(string id, string name, EntityTemplate template) : base(id, name, template.Size)
    {
        Plugins.Add(new BodyPlugin());
    }

    public void ApplyDamage(int amount)
    {
        Plugins.FirstOrDefault<StructurePlugin>()?.ApplyDamage(amount);
    }

    public void HandleAction(ClientAction action)
    {
        if (!string.IsNullOrEmpty(action.Meta.Module))
        {
            Modules.Get(action.Meta.Module).HandleAction(action, this);
        }
    }

    public void Update(TimeSpan deltaT, World world)
    {
        Plugins.OnTick(deltaT, this, world);
        Modules.Update(deltaT, this, world);
    }
}
