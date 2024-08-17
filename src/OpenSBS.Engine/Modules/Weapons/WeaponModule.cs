using OpenSBS.Engine.Actions;
using OpenSBS.Engine.Models;
using OpenSBS.Engine.Modules.Sensors;
using OpenSBS.Engine.Utils;

namespace OpenSBS.Engine.Modules.Weapons;

public class WeaponModule(WeaponTemplate template) : SpaceshipModule<WeaponTemplate>(template)
{
    private const string EngageAction = "engage";
    private const string DisengageAction = "disengage";

    public readonly CountdownTimer Timer = new();
    public string? Target { get; private set; }

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
