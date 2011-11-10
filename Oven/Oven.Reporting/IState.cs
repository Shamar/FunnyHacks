using System;
using System.IO;

namespace Oven.Reporting
{
    public interface IState
    {
        Temperature ActualTemperature { get; }
        
        Stream Photo { get; }
    }
}

