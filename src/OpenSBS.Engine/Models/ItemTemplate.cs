namespace OpenSBS.Engine.Models;

public abstract record ItemTemplate(
    string Id,
    string Name
) : EntityTemplate(Id, Name, 1);
