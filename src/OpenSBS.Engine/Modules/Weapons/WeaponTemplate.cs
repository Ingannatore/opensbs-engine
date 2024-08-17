using OpenSBS.Engine.Models;

namespace OpenSBS.Engine.Modules.Weapons;

public abstract record WeaponTemplate(
    string Id,
    string Name,
    int Size,
    int Damage,
    int Range,
    int AmmoPerCycle,
    int MagazineSize,
    int CycleTime,
    int ReloadTime
) : EntityTemplate(Id, Name, Size);
