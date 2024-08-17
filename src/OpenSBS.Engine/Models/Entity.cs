namespace OpenSBS.Engine.Models;

public abstract class Entity(string id, string name, EntityTemplate template)
{
    public readonly string Id = id;
    public readonly string Name = name;
    public readonly EntityTemplate Template = template;
}
