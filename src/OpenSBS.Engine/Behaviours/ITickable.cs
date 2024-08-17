using OpenSBS.Engine.Models;

namespace OpenSBS.Engine.Behaviours;

public interface ITickable
{
    public void OnTick(World world, Entity owner, TimeSpan deltaT);
}
