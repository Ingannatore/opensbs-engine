namespace OpenSBS.Engine.Modules.Engines;

public abstract record EngineTemplate(
    string Code,
    string Name,
    int Size,
    int MaximumSpeed,
    int Acceleration,
    int Deceleration,
    int RotationSpeed
) : SpaceshipModuleTemplate(Code, Name, Size, SpaceshipModuleCategory.Engine);
