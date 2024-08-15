using OpenSBS.Engine.Models;
using OpenSBS.Engine.Models.Items;

namespace OpenSBS.Engine.Modules.Weapons.Automata;

public class ReloadState : WeaponState
{
    private readonly ItemStack _ammo;

    public static ReloadState Create(ItemStack ammo) => new(ammo);

    private ReloadState(ItemStack ammo) : base("state.reload")
    {
        _ammo = ammo;
    }

    public override void OnEnter(WeaponModule module)
    {
        module.Timer.Reset(module.Template.ReloadTime);
    }

    public override WeaponState? Update(TimeSpan deltaT, WeaponModule module, Celestial owner, World world)
    {
        module.Timer.Advance(deltaT.TotalSeconds);
        if (!module.Timer.IsCompleted)
        {
            return null;
        }

        var remainingAmmo = module.Reload(_ammo);
        if (remainingAmmo != null)
        {
            //owner.Cargo.Add(remainingAmmo);
        }

        return FireState.Create();
    }
}
