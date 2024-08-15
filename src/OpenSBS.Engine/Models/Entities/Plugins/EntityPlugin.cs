namespace OpenSBS.Engine.Models.Entities.Plugins;

public abstract class EntityPlugin : IEquatable<EntityPlugin>
{
    public bool Equals(EntityPlugin? other) => GetType().Name == other?.GetType().Name;
    public override bool Equals(object? obj) => Equals(obj as EntityPlugin);
    public override int GetHashCode() => GetType().Name.GetHashCode();

    public abstract void OnTick(TimeSpan deltaT, SpaceEntity owner, World world);
}
