using OpenSBS.Engine.Models;

namespace OpenSBS.Engine.Modules.Weapons.Automata;

public class FireState : WeaponState
{
    public static FireState Create()
    {
        return new FireState();
    }

    private FireState() : base("state.fire")
    {
    }

    public override WeaponState Update(TimeSpan deltaT, WeaponModule module, SpaceEntity owner, World world)
    {
        if (!WeaponHasTarget(module, world))
        {
            return IdleState.Create();
        }

        if (module.IsTargetUnreachable())
        {
            return OutOfRangeState.Create();
        }

        if (module.IsMagazineEmpty())
        {
            return RequireAmmoState.Create(module.Magazine.AmmoId);
        }

        module.ConsumeAmmo();
        world.DamageEntity(module.Target!.Id, module.Template.Damage);

        return CycleState.Create();
    }
}
