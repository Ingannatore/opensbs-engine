using OpenSBS.Engine.Commands;
using OpenSBS.Engine.Entities;

namespace OpenSBS.Engine.Modules.Sensors;

public class SensorsModule(SensorsTemplate template) : SpaceshipModule
{
    private const string LockTargetAction = "lockTarget";
    private const string UnlockTargetAction = "unlockTarget";

    public readonly SensorsTemplate Template = template;
    public readonly SensorTraceCollection Traces = new();

    public SensorTrace? GetTrace(string id) => Traces.Get(id);

    public override void OnCommand(ModuleCommand command)
    {
        switch (command.Type)
        {
            case LockTargetAction:
                // TODO: implement
                break;

            case UnlockTargetAction:
                // TODO: implement
                break;
        }
    }

    public override void OnTick(World world, Spaceship owner, TimeSpan deltaT)
    {
        Traces.OnTick(owner, world.FindSpaceships(owner.Body.Position, Template.Range));
    }
}
