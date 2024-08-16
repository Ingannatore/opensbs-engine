namespace OpenSBS.Engine.Models;

interface ITickable<T> where T : Entity
{
    public void OnTick(World world, T owner, TimeSpan deltaT);
}
