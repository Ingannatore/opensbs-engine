namespace OpenSBS.Engine.Modules.Weapons;

public abstract record WeaponTemplate(
    string Code,
    string Name,
    int Size,
    int Damage,
    int Range,
    int AmmoPerCycle,
    int MagazineSize,
    int CycleTime,
    int ReloadTime
) : SpaceshipModuleTemplate(Code, Name, Size, SpaceshipModuleCategory.Weapon);
