namespace OpenSBS.Engine.Models.Plugins;

public abstract class EntityPlugin : IEquatable<EntityPlugin>, ITickable<Celestial>
{
    public bool Equals(EntityPlugin? other) => GetType().Name == other?.GetType().Name;
    public override bool Equals(object? obj) => Equals(obj as EntityPlugin);
    public override int GetHashCode() => GetType().Name.GetHashCode();
    public abstract void OnTick(World world, Celestial owner, TimeSpan deltaT);
}
