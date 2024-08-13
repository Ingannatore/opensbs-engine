using OpenSBS.Engine.Automata;

namespace OpenSBS.Engine.Modules.Weapons.Automata;

public abstract class WeaponState(string id) : ModuleState<WeaponModule, WeaponState>
{
    public string Id { get; } = id;

    protected bool WeaponHasTarget(WeaponModule module, World world) =>
        module.HasTarget() && world.ExistsEntity(module.Target!.Id);
}
