using System.Numerics;
using OpenSBS.Engine.Entities;
using OpenSBS.Engine.Utils;

namespace OpenSBS.Engine.Modules.Sensors;

public class SensorTrace(string id)
{
    public readonly string Id = id;
    public SensorTraceStatus Status { get; private set; } = SensorTraceStatus.InRange;
    public double Bearing { get; private set; }
    public int Distance { get; private set; }
    public Vector3 RelativePosition { get; private set; }
    public double RelativeBearing { get; private set; }

    public bool IsOutOfRange => Status == SensorTraceStatus.OutOfRange;
    public void MarkAsOutOfRange() => Status = SensorTraceStatus.OutOfRange;
    public void Update(Spaceship owner, Spaceship target)
    {
        Status = SensorTraceStatus.InRange;
        Bearing = target.Body.Bearing;
        Distance = Vectors.Distance(owner.Body.Position, target.Body.Position);
        RelativePosition = target.Body.Position - owner.Body.Position;
        RelativeBearing = Angles.GetBearing(Vector3.Normalize(RelativePosition));
    }
}
