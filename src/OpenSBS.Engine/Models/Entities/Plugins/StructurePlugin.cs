using OpenSBS.Engine.Utils;

namespace OpenSBS.Engine.Models.Entities.Plugins;

public class StructurePlugin(int hitPoints) : EntityPlugin
{
    public static string Key = "plugin.structure";
    public HitPoints HitPoints { get; } = new(hitPoints, hitPoints);

    public void Repair(int amount) => HitPoints.Inc(amount);
    public void ApplyDamage(int amount) => HitPoints.Dec(amount);

    public override void OnTick(TimeSpan deltaT, SpaceEntity owner, World world)
    {
        if (HitPoints.IsZero)
        {
            owner.OnDestroyed();
        }
    }
}
