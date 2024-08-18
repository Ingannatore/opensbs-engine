using OpenSBS.Engine.Commands;
using OpenSBS.Engine.Entities;
using OpenSBS.Engine.Utils;

namespace OpenSBS.Engine.Modules.Shields;

public class ShieldModule(ShieldTemplate template) : SpaceshipModule
{
    private const string ToggleAction = "toggle";
    private readonly CountdownTimer _countdownTimer = new();

    public readonly ShieldTemplate Template = template;
    public bool IsRaised { get; protected set; } = false;
    public ShieldSector Sector { get; } = new ShieldSector(template.Capacity, template.RechargeRate);

    public override void OnCommand(ModuleCommand command)
    {
        switch (command.Type)
        {
            case ToggleAction:
                IsRaised = !IsRaised;
                if (IsRaised)
                {
                    _countdownTimer.Reset(1);
                }

                break;
        }
    }

    public override void OnTick(World world, Spaceship owner, TimeSpan deltaT)
    {
        if (!IsRaised)
        {
            return;
        }

        _countdownTimer.Advance(deltaT.TotalSeconds);
        if (!_countdownTimer.IsCompleted)
        {
            return;
        }

        Sector.Update();
        _countdownTimer.Reset(1);
    }
}
