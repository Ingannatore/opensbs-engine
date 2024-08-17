namespace OpenSBS.Engine.Modules.Sensors;

public abstract record SensorsTemplate(
    string Code,
    string Name,
    int Size,
    int Range
) : SpaceshipModuleTemplate(Code, Name, Size, SpaceshipModuleCategory.Sensors);
