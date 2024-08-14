using OpenSBS.Engine.Models.Actions;

namespace OpenSBS.Engine.Models.Modules;

public interface IModule
{
    public string Id { get; }
    public string Type { get; }
    public string Name { get; }
    public string ShortName { get; }

    public void HandleAction(ClientAction action, SpaceEntity owner);
    public void Update(TimeSpan deltaT, SpaceEntity owner, World world);
}
