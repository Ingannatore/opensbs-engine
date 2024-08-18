using OpenSBS.Engine.Commands;
using OpenSBS.Engine.Entities;
using OpenSBS.Engine.Modules.Sensors;
using OpenSBS.Engine.Utils;

namespace OpenSBS.Engine.Modules.Weapons;

public class WeaponModule(WeaponTemplate template) : SpaceshipModule
{
    private const string EngageAction = "engage";
    private const string DisengageAction = "disengage";

    public readonly WeaponTemplate Template = template;
    public readonly CountdownTimer Timer = new();
    public string? Target { get; private set; }

    public bool HasTarget() => Target != null;
    public void ResetTimer() => Timer.Reset(Template.CycleTime);
    public void ResetTarget() => Target = null;

    public override void OnCommand(ModuleCommand command)
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
        var targetTrace = (owner as Spaceship)?.Modules.FirstOrDefault<SensorsModule>()?.GetTrace(Target);
    }
}
