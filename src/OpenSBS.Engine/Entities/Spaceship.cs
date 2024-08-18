using System.Numerics;
using OpenSBS.Engine.Behaviours;
using OpenSBS.Engine.Commands;
using OpenSBS.Engine.Components;
using OpenSBS.Engine.Modules;

namespace OpenSBS.Engine.Entities;

public class Spaceship(
    string id,
    string name,
    SpaceshipTemplate template,
    Vector3 position,
    Vector3 direction
) : Entity(id, name), ICommandable
{
    public readonly SpaceshipTemplate Template = template;
    public readonly BodyComponent Body = new(position, direction);
    public readonly StructureComponent Structure = new(template.HitPoints);
    public readonly CargoComponent Cargo = new(template.Size);
    public readonly SpaceshipModuleCollection Modules = new(); // TODO: add modules as configured in the template

    public void OnCommand(Command command)
    {
        if (!command.HasMetadata) throw new Exception("Missing command metadata on a spaceship command");
        if (command.Meta?.EntityId != Id) throw new Exception("Mismatched entity ID on a spaceship command");

        Guid moduleId = command.Meta?.ModuleId ?? throw new Exception("Missing module ID on a spaceship command");
        Modules.Get(moduleId).OnCommand(command);
    }

    public void OnTick(World world, TimeSpan deltaT)
    {
        Body.OnTick(world, this, deltaT);
        Modules.OnTick(world, this, deltaT);
    }
}
