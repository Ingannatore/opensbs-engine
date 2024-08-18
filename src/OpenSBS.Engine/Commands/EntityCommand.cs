namespace OpenSBS.Engine.Commands;

public class EntityCommand(string entityId, string type, object payload) : Command(type, payload)
{
    public readonly string EntityId = entityId;
}
