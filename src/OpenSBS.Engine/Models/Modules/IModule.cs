using OpenSBS.Engine.Models.Entities;

namespace OpenSBS.Engine.Models.Modules;

public interface IModule
{
    public string Id { get; }
    public string Type { get; }
    public string Name { get; }
    public string ShortName { get; }

    public void HandleAction(ClientAction action, Entity owner);
    public void Update(TimeSpan deltaT, Entity owner, World world);
}
