namespace OpenSBS.Engine.Modules.Shields;

public abstract record ShieldTemplate(
    string Code,
    string Name,
    int Size,
    int Capacity,
    int RechargeRate
) : SpaceshipModuleTemplate(Code, Name, Size, SpaceshipModuleCategory.Shield);
