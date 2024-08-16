using System.Numerics;
using OpenSBS.Engine.Models.Plugins;
using OpenSBS.Engine.Utils;

namespace OpenSBS.Engine.Models.Traces;

public class TraceSpatialData
{
    public Vector3 Position { get; protected set; }
    public Vector2 Sector { get; protected set; }
    public double Bearing { get; protected set; }
    public int Distance { get; protected set; }
    public int Speed { get; protected set; }
    public Vector3 RelativePosition { get; protected set; }
    public Vector3 RelativeSectorPosition { get; protected set; }
    public double RelativeBearing { get; protected set; }

    public bool IsOutOfRange(int range)
    {
        return Distance > range;
    }

    public void Update(Celestial owner, Celestial target)
    {
        var ownerBody = owner.Plugins.GetBody();
        var targetBody = target.Plugins.GetBody();

        Position = targetBody.Position;
        Sector = Vectors.ToSector(targetBody.Position);
        Bearing = targetBody.Bearing;
        Distance = (int)Math.Round(Vector3.Distance(ownerBody.Position, targetBody.Position));
        Speed = (int)Math.Round(targetBody.LinearSpeed);
        RelativePosition = targetBody.Position - ownerBody.Position;
        RelativeSectorPosition = targetBody.Position - Vectors.ToSectorCenter(Sector);

        var relativeDirection = Vector3.Normalize(RelativePosition);
        RelativeBearing = Angles.GetBearing(relativeDirection);
    }
}
