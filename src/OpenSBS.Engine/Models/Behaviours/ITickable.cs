namespace OpenSBS.Engine.Models.Behaviours;

public interface ITickable
{
    public void OnTick(World world, Entity owner, TimeSpan deltaT);
}
