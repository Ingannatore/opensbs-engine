using OpenSBS.Engine.Commands;
using OpenSBS.Engine.Entities;
using OpenSBS.Engine.Traces;

namespace OpenSBS.Engine.Modules.Sensors;

public class SensorsModule(SensorsTemplate template) : SpaceshipModule
{
    private const string ScanCompletedAction = "scanCompleted";

    public readonly SensorsTemplate Template = template;
    public readonly EntityTraceCollection Traces = new();

    public EntityTrace? GetTrace(string entityId) => Traces.Get(entityId);

    public override void OnCommand(Command command)
    {
        switch (command.Type)
        {
            case ScanCompletedAction:
                Traces.CompleteScansion(command.PayloadTo<string>()!);
                break;
        }
    }

    public override void OnTick(World world, Entity owner, TimeSpan deltaT)
    {
        foreach (var trace in Traces)
        {
            if (!world.Exists(trace.Id))
            {
                Traces.Remove(trace.Id);
            }
        }

        foreach (var entity in world)
        {
            // Traces.Update(owner, entity, Template.Range);
        }
    }
}
