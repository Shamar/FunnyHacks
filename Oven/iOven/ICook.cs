using System;
using Oven;
using Oven.Control;

namespace iOven
{
    public interface ICook
    {
        IOven Oven { get; }
        
        IAlarmBell AlarmBell { get; }

        IReporter Reporter { get; }
    }
}

