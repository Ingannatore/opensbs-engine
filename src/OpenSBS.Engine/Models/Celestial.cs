using OpenSBS.Engine.Models.Actions;
using OpenSBS.Engine.Models.Entities.Plugins;
using OpenSBS.Engine.Models.Templates;

namespace OpenSBS.Engine.Models;

public abstract class Celestial : Entity
{
    public PluginCollection Plugins { get; } = [];

    public Celestial(string id, string name, EntityTemplate template) : base(id, name, template.Size)
    {
        Plugins.Add(new BodyPlugin(), new ModularPlugin());
    }

    public void ApplyDamage(int amount)
    {
        Plugins.FirstOrDefault<StructurePlugin>()?.ApplyDamage(amount);
    }

    public void HandleAction(ClientAction action)
    {
        if (!string.IsNullOrEmpty(action.Meta.Module))
        {
            Plugins.FirstOrDefault<ModularPlugin>()?.Get(action.Meta.Module).HandleAction(action, this);
        }
    }

    public void Update(TimeSpan deltaT, World world)
    {
        Plugins.OnTick(deltaT, this, world);
    }
}
