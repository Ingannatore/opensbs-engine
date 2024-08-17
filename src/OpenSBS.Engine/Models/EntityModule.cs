using OpenSBS.Engine.Models.Actions;
using OpenSBS.Engine.Models.Behaviours;

namespace OpenSBS.Engine.Models;

public abstract class EntityModule<T>(
    string id,
    string name,
    T template
) : Entity(id, name, template.Size), ICommandable, IEntityModule, ITickable where T : EntityTemplate
{
    public T Template = template;

    public abstract void OnCommand(ClientAction command);
    public abstract void OnTick(World world, Entity owner, TimeSpan deltaT);
}
