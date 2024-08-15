﻿using OpenSBS.Engine.Automata;
using OpenSBS.Engine.Models;
using OpenSBS.Engine.Models.Actions;
using OpenSBS.Engine.Models.Items;
using OpenSBS.Engine.Models.Modules;
using OpenSBS.Engine.Models.Templates;
using OpenSBS.Engine.Models.Traces;
using OpenSBS.Engine.Modules.Sensors;
using OpenSBS.Engine.Modules.Weapons.Automata;
using OpenSBS.Engine.Utils;

namespace OpenSBS.Engine.Modules.Weapons;

public class WeaponModule : Module<WeaponModuleTemplate>
{
    private const string EngageAction = "engage";
    private const string DisengageAction = "disengage";
    private const string ReloadAction = "reload";
    private const string UnloadAction = "unload";
    private readonly ModuleStateMachine<WeaponModule, WeaponState> _stateMachine;

    public EntityTrace? Target { get; private set; }
    public WeaponMagazine Magazine { get; }
    public CountdownTimer Timer { get; }
    public string Status => _stateMachine.State.Id;
    public IEnumerable<string> FiringArcs => Template.FiringArcs;

    public static WeaponModule Create(WeaponModuleTemplate template)
    {
        return new WeaponModule(template);
    }

    private WeaponModule(WeaponModuleTemplate template) : base(ModuleType.Weapon, template)
    {
        Magazine = new WeaponMagazine(template.AmmoType, template.MagazineSize);
        Timer = new CountdownTimer();

        _stateMachine = new ModuleStateMachine<WeaponModule, WeaponState>(this, IdleState.Create());
    }

    public bool HasTarget() => Target != null;

    public bool IsTargetUnreachable()
    {
        if (Target == null)
        {
            return false;
        }

        return Target.IsOutOfRange(Template.Range) || Target.IsOutOfFiringArc(Template.FiringArcs);
    }

    public bool IsMagazineEmpty() => Magazine.IsEmpty();

    public ItemStack Reload(ItemStack ammo) => Magazine.Reload(ammo);

    public void ConsumeAmmo() => Magazine.Consume(Template.AmmoPerCycle);

    public int GetMissingAmmoQuantity(string ammoId) => Magazine.AmmoId == ammoId
        ? Template.MagazineSize - Magazine.Quantity
        : Template.MagazineSize;

    public void ResetTimer() => Timer.Reset(Template.CycleTime);

    public void ResetTarget() => Target = null;

    public override void HandleAction(ClientAction action, Celestial owner)
    {
        switch (action.Type)
        {
            case EngageAction:
                {
                    var targetId = action.PayloadTo<string>()!;
                    Target = owner.Modules.FirstOrDefault<SensorsModule>()?.GetTrace(targetId);
                    break;
                }

            case DisengageAction:
                {
                    Target = null;
                    break;
                }

            case ReloadAction:
                {
                    if (!HasTarget())
                    {
                        var ammoId = action.PayloadTo<string>()!;
                        _stateMachine.SetState(this, RequireAmmoState.Create(ammoId));
                    }

                    break;
                }

            case UnloadAction:
                {
                    if (!HasTarget())
                    {
                        _stateMachine.SetState(this, UnloadState.Create());
                    }

                    break;
                }
        }
    }

    public override void Update(TimeSpan deltaT, Celestial owner, World world)
    {
        _stateMachine.Update(deltaT, this, owner, world);
    }
}
