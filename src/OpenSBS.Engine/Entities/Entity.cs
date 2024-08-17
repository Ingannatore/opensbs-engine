using OpenSBS.Engine.Behaviours;

namespace OpenSBS.Engine.Entities;

public abstract class Entity(string id, string name) : ITickable
{
    public readonly string Id = id;
    public readonly string Name = name;

    public abstract void OnTick(World world, Entity owner, TimeSpan deltaT);
}
