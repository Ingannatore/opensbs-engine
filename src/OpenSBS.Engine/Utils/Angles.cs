using System.Numerics;

namespace OpenSBS.Engine.Utils
{
    public static class Angles
    {
        public static double ToRadians(double value) => value * (Math.PI / 180);

        public static double ToDegrees(double value) => value * (180 / Math.PI);

        public static double GetBearing(Vector3 direction)
        {
            var degrees = ToDegrees(Math.Atan2(direction.Z, direction.X));
            var rotatedDegrees = degrees >= -90 ? degrees - 90 : 270 + degrees;
            return rotatedDegrees <= 0 ? -rotatedDegrees : 360 - rotatedDegrees;
        }
    }
}
