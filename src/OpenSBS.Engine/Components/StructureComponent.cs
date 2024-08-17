using OpenSBS.Engine.Behaviours;
using OpenSBS.Engine.Utils;

namespace OpenSBS.Engine.Components;

public class StructureComponent(int hitPoints) : IDamageable
{
    public BoundedValue HitPoints { get; } = new(hitPoints, hitPoints);
    public bool IsDestroyed => HitPoints.IsEmpty;

    public void Repair(int amount) => HitPoints.Inc(amount);
    public void OnDamage(int amount) => HitPoints.Dec(amount);
}
