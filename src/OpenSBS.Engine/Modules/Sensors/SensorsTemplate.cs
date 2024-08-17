using OpenSBS.Engine.Models;

namespace OpenSBS.Engine.Modules.Sensors;

public abstract record SensorsTemplate(
    string Id,
    string Name,
    int Size,
    int Range
) : EntityTemplate(Id, Name, Size);
