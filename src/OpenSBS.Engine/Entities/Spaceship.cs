using System.Numerics;
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
) : Entity(id, name)
{
    public readonly SpaceshipTemplate Template = template;
    public readonly BodyComponent Body = new(position, direction);
    public readonly StructureComponent Structure = new(template.HitPoints);
    public readonly CargoComponent Cargo = new(template.Size);
    public readonly SignatureComponent Signature = new(template.Size);
    public readonly SpaceshipModuleCollection Modules = new(); // TODO: add modules as configured in the template

    public void OnDamage(int amount) => Structure.OnDamage(amount);

    public void OnCommand(EntityCommand command)
    {
        if (command.EntityId != Id) throw new Exception("Mismatched entity ID on a spaceship command");

        if (command is ModuleCommand moduleCommand)
        {
            Modules.OnCommand(moduleCommand);
        }
    }

    public void OnTick(World world, TimeSpan deltaT)
    {
        Body.OnTick(world, this, deltaT);
        Modules.OnTick(world, this, deltaT);
        Signature.OnTick(this);
    }
}
