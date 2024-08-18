namespace OpenSBS.Engine.Commands;

public class ModuleCommand(string moduleId, string entityId, string type, object payload) : EntityCommand(entityId, type, payload)
{
    public readonly Guid ModuleId = Guid.Parse(moduleId);
}
