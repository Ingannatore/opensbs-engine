using OpenSBS.Engine.Actions;

namespace OpenSBS.Engine.Behaviours;

public interface ICommandable
{
    public void OnCommand(ClientAction command);
}
