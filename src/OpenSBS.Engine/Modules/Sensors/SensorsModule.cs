using OpenSBS.Engine.Commands;
using OpenSBS.Engine.Entities;

namespace OpenSBS.Engine.Modules.Sensors;

public class SensorsModule(SensorsTemplate template) : SpaceshipModule
{
    private const string LockTargetCommand = "lockTarget";
    private const string UnlockTargetCommand = "unlockTarget";

    public readonly SensorsTemplate Template = template;
    public readonly SensorTraceCollection Traces = new();

    public override void OnCommand(ModuleCommand command)
    {
        switch (command.Type)
        {
            case LockTargetCommand:
                if (Traces.CountLockedTargets < Template.MaxLockedTargets)
                {
                    GetTraceFromCommand(command).OnLock(Template.Resolution);
                }
                break;

            case UnlockTargetCommand:
                GetTraceFromCommand(command).OnUnlock();
                break;
        }
    }

    public override void OnTick(World world, Spaceship owner, TimeSpan deltaT)
    {
        Traces.OnTick(world.FindSpaceships(owner.Body.Position, Template.Range), owner, this, deltaT);
    }

    private SensorTrace GetTraceFromCommand(ModuleCommand command)
    {
        var traceId = command.PayloadTo<string>() ?? throw new Exception("Missing trace ID on command");
        return Traces.Get(traceId) ?? throw new Exception($"Unknown trace ID: {traceId}");
    }
}
