using System.Numerics;
using OpenSBS.Engine.Actions;
using OpenSBS.Engine.Behaviours;
using OpenSBS.Engine.Components;
using OpenSBS.Engine.Modules;

namespace OpenSBS.Engine.Entities;

public class Spaceship(string id, string name, Vector3 position, Vector3 direction) : Entity(id, name), ICommandable
{
    public readonly BodyComponent Body = new(position, direction);
    public readonly StructureComponent Structure = new(100); // TODO: temporary, taken from the template
    public readonly CargoComponent Cargo = new(3); // TODO: temporary, taken from the template
    public readonly SpaceshipModuleCollection Modules = new(); // TODO: add modules as configured in the template

    public void OnCommand(ClientAction command)
    {
        throw new NotImplementedException();
    }

    public override void OnTick(World world, Entity owner, TimeSpan deltaT)
    {
        Body.OnTick(world, this, deltaT);
        Modules.OnTick(world, this, deltaT);
    }
}
