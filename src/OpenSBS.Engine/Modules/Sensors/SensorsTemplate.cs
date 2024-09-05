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

    /*
     * Ship size          | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 |  9
     * Max locked targets | 1 | 2 | 4 | 4 | 6 | 6 | 8 | 8 | 10
     */
    private static int CalculateMaxLockedTargets(int size) =>
        size > 2 ? 4 + 2 * ((size - 3) / 2) : size;
}
