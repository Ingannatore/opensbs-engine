using OpenSBS.Engine.Models;

namespace OpenSBS.Engine.Modules.Shields;

public abstract record ShieldTemplate(
    string Id,
    string Name,
    int Size,
    int Capacity,
    int RechargeRate
) : EntityTemplate(Id, Name, Size);
