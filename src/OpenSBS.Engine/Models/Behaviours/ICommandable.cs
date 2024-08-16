using OpenSBS.Engine.Models.Actions;

namespace OpenSBS.Engine.Models.Behaviours;

interface ICommandable
{
    public void OnCommand(ClientAction command);
}
