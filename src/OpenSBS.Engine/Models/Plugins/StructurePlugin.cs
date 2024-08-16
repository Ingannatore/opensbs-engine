using OpenSBS.Engine.Models.Behaviours;
using OpenSBS.Engine.Utils;

namespace OpenSBS.Engine.Models.Plugins;

public class StructurePlugin(int hitPoints) : EntityPlugin, IDamageable
{
    public BoundedValue HitPoints { get; } = new(hitPoints, hitPoints);
    public bool IsDestroyed => HitPoints.IsEmpty;

    public void Repair(int amount) => HitPoints.Inc(amount);
    public void OnDamage(int amount) => HitPoints.Dec(amount);
}
