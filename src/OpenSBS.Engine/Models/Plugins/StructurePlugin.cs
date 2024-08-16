using OpenSBS.Engine.Utils;

namespace OpenSBS.Engine.Models.Plugins;

public class StructurePlugin(int hitPoints) : EntityPlugin
{
    public static string Key = "plugin.structure";
    public BoundedValue HitPoints { get; } = new(hitPoints, hitPoints);
    public bool IsDestroyed => HitPoints.IsEmpty;

    public void Repair(int amount) => HitPoints.Inc(amount);
    public void ApplyDamage(int amount) => HitPoints.Dec(amount);

    public override void OnTick(TimeSpan deltaT, Celestial owner, World world)
    {
        if (HitPoints.IsEmpty)
        {
            // TODO: what to do? is this needed?
        }
    }
}
