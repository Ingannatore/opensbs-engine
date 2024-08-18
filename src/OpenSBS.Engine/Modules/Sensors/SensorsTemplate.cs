using OpenSBS.Engine.Utils;

namespace OpenSBS.Engine.Modules.Sensors;

public abstract record SensorsTemplate(
    string Code,
    string Name,
    int Size,
    Spectra Resolution,
    int Range
) : SpaceshipModuleTemplate(Code, Name, Size, SpaceshipModuleCategory.Sensors)
{
    public readonly int MaxLockedTargets = CalculateMaxLockedTargets(Size);

    private static int CalculateMaxLockedTargets(int size)
    {
        return size > 2 ? 4 + 2 * ((size - 3) / 2) : size;
    }
}
