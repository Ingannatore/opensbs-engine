namespace OpenSBS.Engine.Entities;

public abstract class Entity(string id, string name)
{
    public readonly string Id = id;
    public readonly string Name = name;
}
