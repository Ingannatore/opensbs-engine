namespace OpenSBS.Engine.Models;

public abstract class Entity(string id, string type, int size, string name)
{
    public string Id { get; } = id;
    public string Type { get; } = type;
    public int Size { get; } = size;
    public string Name { get; } = name;
    public bool IsDestroyed { get; private set; }

    public virtual void OnDestroyed() => IsDestroyed = true;
}
