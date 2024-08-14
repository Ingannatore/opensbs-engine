using OpenSBS.Engine.Models;

namespace OpenSBS.Engine.Modules.Weapons.Automata;

public class OutOfRangeState : WeaponState
{
    public static OutOfRangeState Create() => new();

    private OutOfRangeState() : base("state.outOfRange")
    {
    }

    public override void OnEnter(WeaponModule module)
    {
        module.Timer.Reset(0);
    }

    public override WeaponState? Update(TimeSpan deltaT, WeaponModule module, SpaceEntity owner, World world)
    {
        if (!WeaponHasTarget(module, world))
        {
            return IdleState.Create();
        }

        return module.IsTargetUnreachable() ? null : FireState.Create();
    }
}