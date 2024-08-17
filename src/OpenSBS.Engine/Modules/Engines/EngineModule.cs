using OpenSBS.Engine.Models;
using OpenSBS.Engine.Models.Actions;

namespace OpenSBS.Engine.Modules.Engines;

public class EngineModule(EngineTemplate template) : SpaceshipModule<EngineTemplate>(template)
{
    private const string SetThrottleAction = "setThrottle";
    private const string SetRudderAction = "setRudder";

    public int Throttle { get; protected set; }
    public int Rudder { get; protected set; }
    public double TargetSpeed { get; protected set; }

    public override void OnCommand(ClientAction command)
    {
        switch (command.Type)
        {
            case SetThrottleAction:
                Throttle = command.PayloadTo<int>();
                break;
            case SetRudderAction:
                Rudder = command.PayloadTo<int>();
                break;
        }
    }

    public override void OnTick(World world, Entity owner, TimeSpan deltaT)
    {
        var ownerBody = (owner as Spaceship)?.Plugins.GetBody() ?? throw new Exception("Module owner is not a Spaceship");

        ownerBody.Update(
            CalculateLinearSpeed(deltaT, ownerBody.LinearSpeed),
            CalculateAngularSpeed(deltaT)
        );
    }

    private double CalculateAngularSpeed(TimeSpan deltaT)
    {
        var rudderDirection = Math.Sign(Rudder);
        if (rudderDirection == 0)
        {
            return 0;
        }

        return rudderDirection * Template.RotationSpeed * deltaT.TotalSeconds;
    }

    private double CalculateLinearSpeed(TimeSpan deltaT, double currentSpeed)
    {
        TargetSpeed = Template.MaximumSpeed * (Throttle / 100.0);
        var linearSpeedDirection = Math.Sign(TargetSpeed - currentSpeed);

        if (linearSpeedDirection > 0)
        {
            var deltaSpeed = Template.Acceleration * deltaT.TotalSeconds;
            return Math.Min(currentSpeed + deltaSpeed, TargetSpeed);
        }

        if (linearSpeedDirection < 0)
        {
            var deltaSpeed = Template.Deceleration * deltaT.TotalSeconds;
            return Math.Max(currentSpeed - deltaSpeed, TargetSpeed);
        }

        return currentSpeed;
    }
}
