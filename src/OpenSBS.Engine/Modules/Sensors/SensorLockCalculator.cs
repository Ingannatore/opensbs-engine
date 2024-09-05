using OpenSBS.Engine.Utils;

namespace OpenSBS.Engine.Modules.Sensors;

public static class SensorLockCalculator
{
    private static readonly int[] _lockTimes = [1, 4, 6, 8, 10, 15, 25, 40, 60, 90];

    public static TimeSpan? CalculateLockTime(Spectra ownerResolution, Spectra targetSignature)
    {
        var difficulty = new Spectra(
            erm: CalculateLockDifficulty(ownerResolution.Erm, targetSignature.Erm),
            mag: CalculateLockDifficulty(ownerResolution.Mag, targetSignature.Mag),
            grav: CalculateLockDifficulty(ownerResolution.Grav, targetSignature.Grav)
        );

        int?[] times = [
            CalculateLockTime(difficulty.Erm),
            CalculateLockTime(difficulty.Mag),
            CalculateLockTime(difficulty.Grav)
        ];

        var minimumLockTime = times.Min();
        return minimumLockTime == null ? null : new TimeSpan(0, 0, (int)minimumLockTime);
    }

    private static int CalculateLockDifficulty(int resolution, int signature) =>
        Math.Max(0, 5 + resolution - signature);

    private static int? CalculateLockTime(int difficulty)
    {
        if (difficulty > 9) return null;

        return _lockTimes[difficulty];
    }
}
