using OpenSBS.Engine.Models;
using OpenSBS.Engine.Models.Actions;
using OpenSBS.Engine.Models.Modules;
using OpenSBS.Engine.Models.Templates;
using OpenSBS.Engine.Models.Traces;
using OpenSBS.Engine.Modules.Sensors;
using OpenSBS.Engine.Utils;

namespace OpenSBS.Engine.Modules.Weapons;

public class WeaponModule : Module<WeaponModuleTemplate>
{
    private const string EngageAction = "engage";
    private const string DisengageAction = "disengage";

    public EntityTrace? Target { get; private set; }
    public CountdownTimer Timer { get; }
    public IEnumerable<string> FiringArcs => Template.FiringArcs;

    public static WeaponModule Create(WeaponModuleTemplate template)
    {
        return new WeaponModule(template);
    }

    private WeaponModule(WeaponModuleTemplate template) : base(ModuleType.Weapon, template)
    {
        Timer = new CountdownTimer();
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

    public void ResetTimer() => Timer.Reset(Template.CycleTime);

    public void ResetTarget() => Target = null;

    public override void HandleAction(ClientAction action, Celestial owner)
    {
        switch (action.Type)
        {
            case EngageAction:
                {
                    var targetId = action.PayloadTo<string>()!;
                    Target = owner.Plugins.GetModules().FirstOrDefault<SensorsModule>()?.GetTrace(targetId);
                    break;
                }

            case DisengageAction:
                {
                    Target = null;
                    break;
                }
        }
    }

    public override void Update(TimeSpan deltaT, Celestial owner, World world)
    {
    }
}
