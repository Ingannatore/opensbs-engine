using OpenSBS.Engine.Models;
using OpenSBS.Engine.Models.Items;

namespace OpenSBS.Engine.Modules.Weapons.Automata
{
    public class UnloadState : WeaponState
    {
        private ItemStack _ammoToReturn;

        public static UnloadState Create() => new();

        private UnloadState() : base("state.unload")
        {
        }

        public override void OnEnter(WeaponModule module)
        {
            _ammoToReturn = module.Magazine.Unload();

            module.Timer.Reset(module.Template.ReloadTime);
        }

        public override WeaponState? Update(TimeSpan deltaT, WeaponModule module, Celestial owner, World world)
        {
            module.Timer.Advance(deltaT.TotalSeconds);
            if (!module.Timer.IsCompleted)
            {
                return null;
            }

            //owner.Cargo.Add(_ammoToReturn);
            return IdleState.Create();
        }
    }
}
