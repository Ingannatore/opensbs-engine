namespace OpenSBS.Engine.Commands;

public class CommandMetadata(string? entityId, string? moduleId, bool isWebsocket)
{
    public readonly string? EntityId = entityId;
    public readonly Guid? ModuleId = moduleId != null ? Guid.Parse(moduleId) : null;
    public readonly bool IsWebsocket = isWebsocket;
    public readonly bool IsServerRecipient = string.IsNullOrEmpty(entityId) && string.IsNullOrEmpty(moduleId);
}
