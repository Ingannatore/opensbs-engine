
using OpenSBS.Engine.Models.Actions;
using OpenSBS.Engine.Models.Behaviours;
using OpenSBS.Engine.Models.Plugins;

namespace OpenSBS.Engine.Models;

public class Spaceship : Entity, ICommandable, IDamageable, ITickable
{
    public PluginCollection Plugins { get; } = [];

    public Spaceship(string id, string name, int size) : base(id, name, size)
    {
        Plugins.Add(new BodyPlugin(), new ModularPlugin(), new StructurePlugin(999));
    }

    public void OnCommand(ClientAction command)
    {
        throw new NotImplementedException();
    }

    public void OnDamage(int amount) => Plugins.GetStructure().OnDamage(amount);

    public void OnTick(World world, Entity owner, TimeSpan deltaT)
    {
        Plugins.OnTick(world, this, deltaT);
    }
}
