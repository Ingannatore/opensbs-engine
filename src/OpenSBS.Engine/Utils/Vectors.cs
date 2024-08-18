using System.Numerics;

namespace OpenSBS.Engine.Utils;

public static class Vectors
{
    private const int SectorSize = 50000;

    public static int Distance(Vector3 positionA, Vector3 positionB) =>
        (int)Math.Round(Vector3.Distance(positionA, positionB));

    public static Vector3 Rotate(Vector3 value, double yaw, double pitch, double roll)
    {
        var rotationQuaternion = Quaternion.CreateFromYawPitchRoll(
            (float)yaw,
            (float)pitch,
            (float)roll
        );

        return Vector3.Normalize(Vector3.Transform(value, rotationQuaternion));
    }

    public static Vector3 Move(Vector3 position, Vector3 direction, double value) =>
        position + direction * (float)value;

    public static Vector2 ToSector(Vector3 position) =>
        new(
            (float)Math.Floor((position.X + 25000) / SectorSize),
            (float)Math.Floor((position.Z + 25000) / SectorSize)
        );

    public static Vector3 ToSectorCenter(Vector2 sector) =>
        new(
            sector.X * SectorSize,
            0,
            sector.Y * SectorSize
        );
}
