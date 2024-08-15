using OpenSBS.Engine.Models;

namespace OpenSBS.Engine.Modules.Weapons.Automata;

public class IdleState : WeaponState
{
    public static IdleState Create() => new();

    private IdleState() : base("state.idle")
    {
    }

    public override void OnEnter(WeaponModule module)
    {
        module.ResetTarget();
        module.Timer.Reset(0);
    }

    public override WeaponState? Update(TimeSpan deltaT, WeaponModule module, Celestial owner, World world) =>
        WeaponHasTarget(module, world) ? FireState.Create() : null;
}
