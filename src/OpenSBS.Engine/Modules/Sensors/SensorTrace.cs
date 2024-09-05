using System.Numerics;
using OpenSBS.Engine.Entities;
using OpenSBS.Engine.Utils;

namespace OpenSBS.Engine.Modules.Sensors;

public class SensorTrace(Spaceship target)
{
    public readonly string Id = target.Id;
    public SensorTraceStatus Status { get; private set; } = SensorTraceStatus.InRange;
    public Spectra Signature { get; private set; } = new(target.Template.Size);
    public double Bearing { get; private set; }
    public int Distance { get; private set; }
    public Vector3 RelativePosition { get; private set; }
    public double RelativeBearing { get; private set; }
    public CountdownTimer? Timer { get; private set; }

    public bool IsLockingOrLocked => Status == SensorTraceStatus.Locking || Status == SensorTraceStatus.Locked;
    public bool IsOutOfRange => Status == SensorTraceStatus.OutOfRange;

    public void MarkAsOutOfRange()
    {
        Timer = null;
        Status = SensorTraceStatus.OutOfRange;
    }

    public void OnLock(Spectra resolution)
    {
        if (IsLockingOrLocked) return;

        var lockTime = SensorLockCalculator.CalculateLockTime(resolution, Signature);
        if (lockTime != null)
        {
            Timer = new CountdownTimer((TimeSpan)lockTime);
            Status = SensorTraceStatus.Locking;
        }
    }

    public void OnUnlock()
    {
        if (IsLockingOrLocked)
        {
            Timer = null;
            Status = SensorTraceStatus.InRange;
        }
    }

    public void OnTick(Spaceship target, Spaceship owner, TimeSpan deltaT)
    {
        Signature = target.Signature;
        Bearing = target.Body.Bearing;
        Distance = Vectors.Distance(owner.Body.Position, target.Body.Position);
        RelativePosition = target.Body.Position - owner.Body.Position;
        RelativeBearing = Angles.GetBearing(Vector3.Normalize(RelativePosition));

        if (Timer == null)
        {
            Status = SensorTraceStatus.InRange;
            return;
        }

        if (Timer.Advance(deltaT))
        {
            Timer = null;
            Status = SensorTraceStatus.Locked;
        }
    }
}
