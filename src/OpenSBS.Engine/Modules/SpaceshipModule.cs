using OpenSBS.Engine.Commands;
using OpenSBS.Engine.Entities;

namespace OpenSBS.Engine.Modules;

public abstract class SpaceshipModule
{
    public readonly Guid Id = Guid.NewGuid();

    public abstract void OnCommand(Command command);
    public abstract void OnTick(World world, Entity owner, TimeSpan deltaT);
}
