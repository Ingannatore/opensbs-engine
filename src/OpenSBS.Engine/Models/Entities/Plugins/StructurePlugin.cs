using OpenSBS.Engine.Utils;

namespace OpenSBS.Engine.Models.Entities.Plugins;

public class StructurePlugin(int hitPoints) : IEntityPlugin
{
    public static string Key = "plugin.structure";
    public BoundedValue HitPoints { get; } = new(hitPoints, hitPoints);

    public void Repair(int amount) => HitPoints.Inc(amount);
    public void ApplyDamage(int amount) => HitPoints.Dec(amount);

    public void OnTick(TimeSpan deltaT, SpaceEntity owner, World world)
    {
        if (HitPoints.IsZero)
        {
            owner.OnDestroyed();
        }
    }
}
