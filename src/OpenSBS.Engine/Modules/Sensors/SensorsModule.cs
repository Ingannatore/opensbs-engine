using OpenSBS.Engine.Models;
using OpenSBS.Engine.Models.Actions;
using OpenSBS.Engine.Models.Modules;
using OpenSBS.Engine.Models.Traces;

namespace OpenSBS.Engine.Modules.Sensors;

public class SensorsModule : EntityModule<SensorsTemplate>
{
    private const string ScanCompletedAction = "scanCompleted";

    public EntityTraceCollection Traces { get; }
    public int Range => Template.Range;

    public static SensorsModule Create(SensorsTemplate template)
    {
        return new SensorsModule(template);
    }

    private SensorsModule(SensorsTemplate template) : base(ModuleType.Sensors, template)
    {
        Traces = new EntityTraceCollection();
    }

    public EntityTrace? GetTrace(string entityId) => Traces.Get(entityId);

    public override void OnCommand(ClientAction command)
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
            if (!world.ExistsEntity(trace.Id))
            {
                Traces.Remove(trace.Id);
            }
        }

        foreach (var entity in world)
        {
            Traces.Update(owner, entity, Template.Range);
        }
    }
}
