using OpenSBS.Engine.Models.Actions;
using OpenSBS.Engine.Models.Behaviours;
using OpenSBS.Engine.Models.Templates;

namespace OpenSBS.Engine.Models;

public abstract class EntityModule<T>(
    string id,
    T template
) : Entity(id, template.Name, template.Size), ICommandable, IEntityModule, ITickable where T : ModuleTemplate
{
    public T Template = template;

    public abstract void OnCommand(ClientAction command);
    public abstract void OnTick(World world, Entity owner, TimeSpan deltaT);
}
