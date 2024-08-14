namespace OpenSBS.Engine.Models.Entities.Plugins;

public interface IEntityPlugin
{
    public void OnTick(TimeSpan deltaT, SpaceEntity owner, World world);
}
