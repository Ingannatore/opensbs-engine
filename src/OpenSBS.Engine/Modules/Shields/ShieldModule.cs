﻿using OpenSBS.Engine.Models;
using OpenSBS.Engine.Models.Actions;
using OpenSBS.Engine.Models.Modules;
using OpenSBS.Engine.Models.Templates;
using OpenSBS.Engine.Utils;

namespace OpenSBS.Engine.Modules.Shields;

public class ShieldModule : Module<ShieldModuleTemplate>
{
    private const string ToggleAction = "toggle";
    private readonly CountdownTimer _countdownTimer = new();

    public bool IsRaised { get; protected set; } = false;
    public ShieldSector Sector { get; }

    public static ShieldModule Create(ShieldModuleTemplate template) => new(template);

    private ShieldModule(ShieldModuleTemplate template) : base(ModuleType.Shield, template)
    {
        Sector = new ShieldSector(template.Capacity, template.RechargeRate);
    }

    public override void HandleAction(ClientAction action, Celestial owner)
    {
        switch (action.Type)
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

    public override void Update(TimeSpan deltaT, Celestial owner, World world)
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
