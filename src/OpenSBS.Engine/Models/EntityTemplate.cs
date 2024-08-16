namespace OpenSBS.Engine.Models;

public abstract class EntityTemplate(string id, string name, int size)
{
    public readonly string Id = id;
    public readonly string Name = name;
    public readonly int Size = size;
}
