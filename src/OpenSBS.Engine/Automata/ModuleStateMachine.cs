﻿using OpenSBS.Engine.Models;
using OpenSBS.Engine.Models.Modules;

namespace OpenSBS.Engine.Automata;

public class ModuleStateMachine<TM, TS> where TM : IModule where TS : ModuleState<TM, TS>
{
    public TS State { get; private set; }

    public ModuleStateMachine(TM module, TS initialState)
    {
        State = initialState;
        State.OnEnter(module);
    }

    public void SetState(TM module, TS newState)
    {
        State = newState;
        State.OnEnter(module);
    }

    public void Update(TimeSpan deltaT, TM module, Celestial owner, World world)
    {
        var nextState = State.Update(deltaT, module, owner, world);
        if (nextState != null)
        {
            SetState(module, nextState);
        }
    }
}
