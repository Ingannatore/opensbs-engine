using System.Numerics;
using OpenSBS.Engine.Utils;

namespace OpenSBS.Engine.Models.Entities.Plugins;

public class BodyPlugin(Vector3 position, Vector3 direction) : EntityPlugin
{
    public static string Key = "plugin.body";
    public Vector3 Position { get; private set; } = position;
    public Vector3 Direction { get; private set; } = direction;
    public double Bearing { get; private set; } = Angles.GetBearing(direction);
    public double LinearSpeed { get; private set; }
    public double AngularSpeed { get; private set; }

    public BodyPlugin() : this(Vector3.Zero, Vector3.UnitZ) { }

    public override void OnTick(TimeSpan deltaT, Celestial owner, World world)
    {
        RotateBody(deltaT);
        MoveBody(deltaT);
    }

    public void Update(double linearSpeed, double angularSpeed)
    {
        LinearSpeed = linearSpeed;
        AngularSpeed = angularSpeed;
    }

    private void RotateBody(TimeSpan deltaT)
    {
        if (AngularSpeed == 0)
        {
            return;
        }

        var deltaYaw = Angles.ToRadians(AngularSpeed * deltaT.TotalSeconds);
        Direction = Vectors.Rotate(Direction, deltaYaw, 0, 0);

        Bearing = Angles.GetBearing(Direction);
    }

    private void MoveBody(TimeSpan deltaT)
    {
        if (LinearSpeed == 0)
        {
            return;
        }

        var deltaMovement = LinearSpeed * deltaT.TotalSeconds;
        Position = Vectors.Move(Position, Direction, deltaMovement);
    }
}
