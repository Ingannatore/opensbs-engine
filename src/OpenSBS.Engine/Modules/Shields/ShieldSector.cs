using OpenSBS.Engine.Utils;

namespace OpenSBS.Engine.Modules.Shields;

public class ShieldSector(int capacity, int rechargeRate)
{
    public BoundedValue HitPoints { get; } = new(capacity, capacity);
    public int RechargeRate { get; protected set; } = rechargeRate;
    public double Ratio => HitPoints.Ratio;

    public void Update()
    {
        if (!HitPoints.IsFull)
        {
            HitPoints.Inc(RechargeRate);
        }
    }
}
