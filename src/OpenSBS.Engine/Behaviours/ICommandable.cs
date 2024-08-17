using OpenSBS.Engine.Commands;

namespace OpenSBS.Engine.Behaviours;

public interface ICommandable
{
    public void OnCommand(Command command);
}
