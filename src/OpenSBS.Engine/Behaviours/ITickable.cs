using OpenSBS.Engine.Entities;

namespace OpenSBS.Engine.Behaviours;

public interface ITickable
{
    public void OnTick(World world, Entity owner, TimeSpan deltaT);
}
