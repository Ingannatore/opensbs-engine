﻿using OpenSBS.Engine.Commands;
using OpenSBS.Engine.Entities;

namespace OpenSBS.Engine.Modules.Engines;

public class EngineModule(EngineTemplate template) : SpaceshipModule
{
    private const string SetThrottleAction = "setThrottle";
    private const string SetRudderAction = "setRudder";

    public readonly EngineTemplate Template = template;
    public int Throttle { get; protected set; }
    public int Rudder { get; protected set; }
    public double TargetSpeed { get; protected set; }

    public override void OnCommand(ModuleCommand command)
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

    public override void OnTick(World world, Spaceship owner, TimeSpan deltaT)
    {
        owner.Body.Update(
            CalculateLinearSpeed(deltaT, owner.Body.LinearSpeed),
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
