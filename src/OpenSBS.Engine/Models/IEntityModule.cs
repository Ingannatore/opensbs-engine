using OpenSBS.Engine.Models.Actions;

namespace OpenSBS.Engine.Models;

public interface IEntityModule
{
    public void OnCommand(ClientAction command);
    public void OnTick(World world, Entity owner, TimeSpan deltaT);
}
