using OpenSBS.Engine.Models;

namespace OpenSBS.Engine.Modules.Weapons.Automata;

public class OutOfAmmoState : WeaponState
{
    public static OutOfAmmoState Create() => new();

    private OutOfAmmoState() : base("state.outOfAmmo")
    {
    }

    public override WeaponState? Update(TimeSpan deltaT, WeaponModule module, Celestial owner, World world) => null;
}
