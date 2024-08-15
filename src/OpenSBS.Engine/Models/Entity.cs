namespace OpenSBS.Engine.Models;

public abstract class Entity(string id, string name, int size)
{
    public string Id { get; } = id;
    public string Name { get; } = name;
    public int Size { get; } = size;
}
