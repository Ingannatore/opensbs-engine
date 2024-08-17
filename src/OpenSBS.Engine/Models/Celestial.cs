using OpenSBS.Engine.Behaviours;
using OpenSBS.Engine.Plugins;

namespace OpenSBS.Engine.Models;

public class Celestial : Entity, ITickable
{
    public PluginCollection Plugins { get; } = [];

    public Celestial(string id, string name, EntityTemplate template) : base(id, name, template.Size)
    {
        Plugins.Add(new BodyPlugin());
    }

    public void OnTick(World world, Entity owner, TimeSpan deltaT)
    {
        Plugins.OnTick(world, this, deltaT);
    }
}
