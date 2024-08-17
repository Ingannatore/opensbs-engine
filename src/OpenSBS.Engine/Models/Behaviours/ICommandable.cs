using OpenSBS.Engine.Models.Actions;

namespace OpenSBS.Engine.Models.Behaviours;

public interface ICommandable
{
    public void OnCommand(ClientAction command);
}
