using OpenSBS.Engine.Models;

namespace OpenSBS.Engine.Modules.Weapons.Automata;

public class CycleState : WeaponState
{
    public static CycleState Create() => new();

    private CycleState() : base("state.fire")
    {
    }

    public override void OnEnter(WeaponModule module)
    {
        module.Timer.Reset(module.Template.CycleTime);
    }

    public override WeaponState? Update(TimeSpan deltaT, WeaponModule module, Celestial owner, World world)
    {
        module.Timer.Advance(deltaT.TotalSeconds);
        return module.Timer.IsCompleted ? FireState.Create() : null;
    }
}
