namespace OpenSBS.Engine.Models.Behaviours;

interface ITickable
{
    public void OnTick(World world, Entity owner, TimeSpan deltaT);
}
