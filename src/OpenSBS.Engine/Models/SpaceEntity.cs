using OpenSBS.Engine.Models.Actions;
using OpenSBS.Engine.Models.Entities.Plugins;
using OpenSBS.Engine.Models.Modules;
using OpenSBS.Engine.Models.Templates;

namespace OpenSBS.Engine.Models;

public abstract class SpaceEntity : Entity
{
    public ModulesCollection Modules { get; } = [];
    public PluginCollection Plugins { get; } = [];
    public BodyPlugin Body => Plugins.FirstOrDefault<BodyPlugin>(BodyPlugin.Key) ?? throw new Exception("Space entity does NOT have a body plugin");

    public SpaceEntity(string id, string name, EntityTemplate template) : base(id, template.Type, name)
    {
        Plugins.Add(BodyPlugin.Key, new BodyPlugin());
    }

    public void ApplyDamage(int amount)
    {
        Plugins.FirstOrDefault<StructurePlugin>(StructurePlugin.Key)?.ApplyDamage(amount);
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
