using OpenSBS.Engine.Models;

namespace OpenSBS.Engine.Modules.Engines;

public abstract record EngineTemplate(
    string Id,
    string Name,
    int Size,
    int MaximumSpeed,
    int Acceleration,
    int Deceleration,
    int RotationSpeed
) : EntityTemplate(Id, Name, Size);
