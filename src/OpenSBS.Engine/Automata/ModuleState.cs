using OpenSBS.Engine.Models;

namespace OpenSBS.Engine.Automata;

public abstract class ModuleState<TM, TS>
{
    public virtual void OnEnter(TM module)
    {
    }

    public abstract TS? Update(TimeSpan deltaT, TM module, SpaceEntity owner, World world);
}
