namespace OpenSBS.Engine.Modules;

public abstract record SpaceshipModuleTemplate(
    string Code,
    string Name,
    int Size,
    string Category
);
