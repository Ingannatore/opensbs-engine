using OpenSBS.Engine.Models;
using OpenSBS.Engine.Models.Actions;
using OpenSBS.Engine.Models.Modules;
using OpenSBS.Engine.Modules.Sensors;
using OpenSBS.Engine.Utils;

namespace OpenSBS.Engine.Modules.Weapons;

public class WeaponModule : EntityModule<WeaponTemplate>
{
    private const string EngageAction = "engage";
    private const string DisengageAction = "disengage";

    public string? Target { get; private set; }
    public CountdownTimer Timer { get; }

    public static WeaponModule Create(WeaponTemplate template)
    {
        return new WeaponModule(template);
    }

    private WeaponModule(WeaponTemplate template) : base(ModuleType.Weapon, template)
    {
        Timer = new CountdownTimer();
    }

    public bool HasTarget() => Target != null;
    public void ResetTimer() => Timer.Reset(Template.CycleTime);
    public void ResetTarget() => Target = null;

    public override void OnCommand(ClientAction command)
    {
        switch (command.Type)
        {
            case EngageAction:
                {
                    Target = command.PayloadTo<string>()!;
                    break;
                }

            case DisengageAction:
                {
                    Target = null;
                    break;
                }
        }
    }

    public override void OnTick(World world, Entity owner, TimeSpan deltaT)
    {
        var targetTrace = (owner as Spaceship)?.Plugins.GetModules().FirstOrDefault<SensorsModule>()?.GetTrace(Target);
    }
}
