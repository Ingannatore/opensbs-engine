using System.Numerics;
using OpenSBS.Engine.Behaviours;
using OpenSBS.Engine.Entities;
using OpenSBS.Engine.Utils;

namespace OpenSBS.Engine.Components;

public class BodyComponent(Vector3 position, Vector3 direction) : ITickable
{
    public Vector3 Position { get; private set; } = position;
    public Vector3 Direction { get; private set; } = direction;
    public double Bearing { get; private set; } = Angles.GetBearing(direction);
    public double LinearSpeed { get; private set; }
    public double AngularSpeed { get; private set; }

    public BodyComponent() : this(Vector3.Zero, Vector3.UnitZ) { }
    public BodyComponent(Vector3 position) : this(position, Vector3.UnitZ) { }

    public void OnTick(World world, Entity owner, TimeSpan deltaT)
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
