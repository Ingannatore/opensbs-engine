using OpenSBS.Engine.Models.Entities;
using OpenSBS.Engine.Models.Templates;

namespace OpenSBS.Engine.Models.Modules;

public abstract class Module<T>(string type, T template) : IModule where T : ModuleTemplate
{
    public string Id { get; } = Guid.NewGuid().ToString("N");
    public string Type { get; } = type;
    public T Template { get; } = template;
    public string Name => Template.Name;
    public string ShortName => Template.ShortName;

    public abstract void HandleAction(ClientAction action, Entity owner);
    public abstract void Update(TimeSpan deltaT, Entity owner, World world);
}
